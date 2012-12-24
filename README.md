#twitter.bootstrap.mvc 
===================================================
This is the [nuget](http://nuget.org/) package for quickly adding [Twitter Bootstrap](http://twitter.github.com/bootstrap/) to an [ASP.Net MVC 4](http://www.asp.net/mvc) application. 

See the overview [blog post](http://lostechies.com/erichexter/2012/11/20/twitter-bootstrap-mvc4-the-template-nuget-package-for-asp-net-mvc4-projects/) for screen shots and features. 
This is a User Interface project that does not require a specific data access or architecture for you MVC applications.  The author has some opinions but those are kept in another project that builds on top of this UI package.

##Features
* JS and CSS bundling/minification of Twitter Bootstrap files the MVC4 way
* Incorporate a jQuery validation fix to work with the bootstrap javascript
* Razor Layout templates using Twitter Bootstrap markup.
* Menus using Navigation Routes, including submenus and hiding menus by context(logged in vs anonymous)
* Runtime Scaffolding – default Index, Edit and Detail views.. You provide the [POCOs](http://en.wikipedia.org/wiki/Plain_Old_CLR_Object) and we will render the [CRUD](http://en.wikipedia.org/wiki/Create,_read,_update_and_delete) views.
* [Post Redirect Get](http://en.wikipedia.org/wiki/Post/Redirect/Get) support using the Bootstrap Alert styles.
* A Sample to show how to use all of this stuff

Things we are working on:
* MVC code templates to generate new views from the mvc add view / add controller dialogs
* Strongly typed Html Helpers to render bootstrap concepts like icons


##Install
To view a working sample, install the [twitter.bootstrap.mvc4.sample](http://nuget.org/packages/twitter.bootstrap.mvc4.sample).

	> Install-Package twitter.bootstrap.mvc4
	> Install-Package twitter.bootstrap.mvc4.sample
	> Install-Package twitter.bootstrap.mvc4.templates //for MVC Code Templates..(still a work in progress)

** Preview Releases: ** The preview releases are on this nuget feed (http://www.myget.org/F/erichexter/)

** Build Status: ** 
<a href="http://teamcity.codebetter.com/viewType.html?buildTypeId=bt676&guest=1"><img src="http://teamcity.codebetter.com/app/rest/builds/buildType:(id:bt676)/statusIcon"/></a> 


Brought to you by [Eric Hexter](http://lostechies.com/erichexter/)
