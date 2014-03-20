CkanFrontEnd-DotNet
===================

###About:

  The project was created by the Accela aimed at helping CivicData users create their own front end based on             civicdata.com.
  
  The project provides fully function based on a organization shares on CivicData. 

###Enviroment:
* Visual Studio 2013
* .NET Framework 4.5
* ASP.NET MVC 4
  

###Installation:

 __Step 1.__ Create organization and  publish your open data on civicdata.
  
    You can publish your open data on the Admin portal(http://admin.accela.com) or upload your open data on the Civic   
    Data(http://civicdata.com). 

 __Step 2.__ Download the solution from GitHub.
  
 __Step 3.__ Make changes to the configuration.
  
    Found the configuration file Web.config:
    
![Web.config](https://github.com/Accela-Inc/CivicIDOAuthClient-DotNet/blob/screenshots/screenshots/authconfig.png?raw=true 'Web.config')

1.Change it to your own organization.

![Web.config](https://github.com/Accela-Inc/CkanFrontEnd-DotNet/blob/master/Screenshots/Configs/OrganizationConfig.png?raw=true 'Web.config')

2.Other configurations (Change if your need)

* Theme  settings

![Web.config](https://github.com/Accela-Inc/CkanFrontEnd-DotNet/blob/master/Screenshots/Configs/ThemeSettings.png?raw=true 'Web.config')


* Home page settings

![Web.config](https://github.com/Accela-Inc/CkanFrontEnd-DotNet/blob/master/Screenshots/Configs/HomePageSettings.png?raw=true 'Web.config')

  Warning:

	  To make the page more meanningful you are  suggested to complete the information for the dataset in civicdata.com. 
	  Ex. Tags, Source, Version, Author and so on.  
	  If you have some featured datasets, you can set “Home.FeaturedPackagesEnable” equal to true, 
	  set “Home.FeaturedPackagesTag” and add it as tag into the featured datasets, It will works. 
	  The “Home.FeaturedPackagesLimit” defined the featured dataset’s number.
	
* Hidden tags settings
	
	
![Web.config](https://github.com/Accela-Inc/CkanFrontEnd-DotNet/blob/master/Screenshots/Configs/ThemeSettings.png?raw=true 'Web.config')

  Hidden tags which your don not want to show on the front end.


* Package page settings


![Web.config](https://github.com/Accela-Inc/CkanFrontEnd-DotNet/blob/master/Screenshots/Configs/PackagePageSettings.png?raw=true 'Web.config')

* Third-party plug-ins

  UserVoice, Disqus, AddThis, GoogleAnalytics was used in this project.

  Configuration and usage please refer to the Web.config.

* Caching and Log settings

  Configuration and usage please refer to the Web.config

* More configurations please see the web.config file.

* Third-party plug-ins

  UserVoice, Disqus, AddThis, GoogleAnalytics was used in this project.

  Configuration and usage please refer to the Web.config.

* Caching and Log settings

  Configuration and usage please refer to the Web.config

* More configurations please see the web.config file.

__Step 4.__ Run the solution(F5)

__Step 5.__ Deployment 

	Warning: When you deploy your front end, the Web.Release.config is in using instead of 
	
	the Web.config,	so make sure the configuration changes changed in Web.Release.config.
	
The demo url : http://opencivicdata.cloudapp.net

If you run into any issues, please post them to the GitHub!










