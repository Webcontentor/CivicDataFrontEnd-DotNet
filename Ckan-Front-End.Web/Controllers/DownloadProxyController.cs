﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using GaDotNet.Common;
using GaDotNet.Common.Data;
using GaDotNet.Common.Helpers;
using Ckan_Front_End.Web.Models.Helpers;
using System.Configuration;
using Ckan_Front_End.Web.Models.ActionResults;
using Ckan_Front_End.Web.Models;
using log4net;
using System.Reflection;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Ckan_Front_End.Web.Controllers
{
    public class DownloadProxyController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Provides a reverse proxy to content that is provided via the
        /// catalog.  Provided to allow capturing of analytics
        /// for downloads that originate from the master catalog website
        /// while also simplifying the setup by allowing download links to go
        /// through the catalog web application.
        /// GET: /content/{path}
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult Index(string path)
        {
            if (SettingsHelper.GetDownloadProxyEnabled())
            {
                return new DelegatingResult(context =>
                {
                    StreamDownload(path);
                });
            }
            else
            {
                throw new DescriptiveHttpException(404, "Resource not found", "The requested file was not found or is no longer available.");
            }
        }

        /// <summary>
        /// Stream the resource to the client
        /// </summary>
        /// <param name="path"></param>
        private void StreamDownload(string path)
        {
            // Get the first element in the path and determine if we have a download proxy configured for that path
            string[] pathElements = path.Split(char.Parse(@"/"), char.Parse(@"\"));
            string rootDirectory = String.Empty;
            if (pathElements.Length > 1)
            {
                rootDirectory = pathElements[0];
            }

            // Get the base proxy location
            bool rootLocationFound = false;
            Uri proxyLocation = SettingsHelper.GetDownloadProxyLocation(rootDirectory, out rootLocationFound);

            // If the root location maps to a download proxy location remove the first part of the path
            if (rootLocationFound)
            {
                path = Regex.Replace(path, String.Format(@"^{0}[\/\\]",rootDirectory), "");
            }
                
            // Get the full uri to the resource
            Uri contentUri = new Uri(proxyLocation, path);

            long bytes = 0;

            // Get the HttpContext for the request going through this controller.
            HttpContextWrapper context = (HttpContextWrapper)HttpContext;

            // Process the download
            if (contentUri.IsFile || contentUri.IsUnc)
            {
                bytes = StreamFromFileResource(context, contentUri);
            }
            else if (contentUri.Scheme == "http" || contentUri.Scheme == "https")
            {
                bytes = StreamFromHttpResource(context, contentUri);
            }

            // Track the download in Google Analytics
            if (bytes > 0 && SettingsHelper.GetGoogleAnalyticsEnabled())
            {
                TrackDownloadEvent(path, context, bytes);
            }
        }

        /// <summary>
        /// Track the download event
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <param name="bytes"></param>
        private static void TrackDownloadEvent(string path, HttpContextWrapper context, long bytes)
        {
            ConfigurationManager.AppSettings["GoogleAnalyticsAccountCode"] = SettingsHelper.GetGoogleAnalyticsProfile();

            var code = GaDotNet.Common.Data.ConfigurationSettings.GoogleAccountCode;

            string referrer = "NoReferrer";
            if (context.Request != null && context.Request.UrlReferrer != null)
            {
                referrer = context.Request.UrlReferrer.Host;
            }

            int kiloBytes = (int)(bytes / 1024);

            GoogleEvent googleEvent = new GoogleEvent("http://localhost.local",
                "Download",
                referrer,
                path,
                kiloBytes);

            var app = (HttpApplication)context.GetService(typeof(HttpApplication));

            TrackingRequest request =
                new RequestFactory().BuildRequest(googleEvent, app.Context);

            GoogleTracking.FireTrackingEvent(request);
        }

        /// <summary>
        /// Stream a file from a file resource.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private long StreamFromFileResource(HttpContextWrapper context, Uri uri)
        {
            long bytes = 0;

            // Get the HttpResponse from this proxied request.
            HttpResponseWrapper response = (HttpResponseWrapper)context.Response;

            if (System.IO.File.Exists(uri.LocalPath))
            {
                response.ContentType = MimeType(uri.LocalPath);
                // Transfer the stream to the response stream
                using (FileStream stream = new FileStream(uri.LocalPath, FileMode.Open))
                {
                    stream.CopyTo(response.OutputStream);
                    response.End();
                }
            }
            else
            {
                //response.StatusCode = 404;
                //response.End();

                throw new DescriptiveHttpException(404, "Resource not found", "The requested file was not found or is no longer available.");
            }

          
            return bytes;
        }

        /// <summary>
        /// Get the mime type of a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string MimeType(string filename)
        {
            string mimeType = "application/octetstream";
            try
            {
                string extension = System.IO.Path.GetExtension(filename).ToLower();
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(extension);
                if (registryKey != null && registryKey.GetValue("Content Type") != null)
                {
                    mimeType = registryKey.GetValue("Content Type").ToString();
                }
            }
            catch (Exception ex)
            {
                log.Warn(String.Format("Unable to determine mime type for file {0}", filename), ex);
            }
            return mimeType;
        } 

        /// <summary>
        /// Stream a file from an HTTP resource.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private long StreamFromHttpResource(HttpContextWrapper context, Uri uri)
        {
            long bytes = 0;

            // Get the HttpResponse from this proxied request.
            HttpResponseWrapper response = (HttpResponseWrapper)context.Response;

            // Create the request for the resource
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.Method = context.Request.HttpMethod;
            req.ServicePoint.Expect100Continue = false;
            req.Timeout = 20000;

            try
            {
                using (WebResponse serverResponse = req.GetResponse())
                {
                    // Set the return content type
                    response.ContentType = serverResponse.ContentType;

                    // Transfer the stream to the response stream
                    using (Stream responseStream = serverResponse.GetResponseStream())
                    {
                        responseStream.CopyTo(response.OutputStream);
                        response.End();
                    }
                }

            }
            catch (WebException ex)
            {
                int statusCode = 404;
                HttpWebResponse errorResponse = (HttpWebResponse)ex.Response;
                if (errorResponse != null)
                {
                    statusCode = (int)errorResponse.StatusCode;
                }

                throw new DescriptiveHttpException(statusCode, errorResponse.StatusDescription, "The requested file was not able to be downloaded.");
            }

            return bytes;
        }
        
    }
}
