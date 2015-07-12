FlickrNet-Experimental
======================

Experimental repository for the next version of the FlickrNet API library.

Currently I am attempting to convert the library into a T4 based project, 
so that seperate DLLs can be created easily targetting different platforms.

For the current stable version go to http://flickrnet.codeplex.com 
or better still just download it from http://www.nuget.org/packages/FlickrNet.

Change Log
==========

July 2015: PCL version released via NuGet. Get [Flickr-4.0.2-alpha](http://www.nuget.org/packages/FlickrNet/4.0.2-alpha) to use.

Feb 2014: Loads of tests being added. I've got the .Net 4.5 branch stable now. 
I have a few more tests I want to migrate over, 
and then I'd like to try creating a portable library version of the library, 
which should reduce the number of seperate configurations I actually need.

September 2013: First initial checkins. 
Currently contains code for most public method calls for both .Net 4.5 and Windows 8
as seperate DLLs. Windows 8 authentication, Upload support, and example Windows 8 app added.

[![Build status](https://ci.appveyor.com/api/projects/status/32k42uv0kdtmfvn1)](https://ci.appveyor.com/project/samjudson/flickrnet-experimental)