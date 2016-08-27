# Flickr-Net

The Flickr.Net API Library is a .Net Library for accessing the Flickr API. 
It is written entirely in C#.

The library provides a simple one-to-one mapping to the methods of the Flickr REST API, 
hopefully hiding all of the complexity of calling the API, especially when it comes to authentication. 
Check the Flickr API web site for the full list of commands, and then use the corresponding method in the Flickr library, 
e.g. to call flickr.photos.search use the Flickr.PhotosSearch method.

The library is not an attempt to provide an ORM layer over the Flickr API, 
e.g. if you retrieve a list of photosets for a user (i.e. by calling Flickr.PhotosetsGetList) 
there is no direct property on each photoset to get the photos for that set, 
you must go back to the Flickr object and call Flickr.PhotosetsGetPhotos passing in the photoset id.

# Getting Started

The FlickrNet API library is available via NuGet.org. SImply run the following command:

~~~
Install-Package FlickrNet
~~~

# Examples

You can create a new instance of the Flickr class, and set its properties, or you can use one of the parameterised constructors:

~~~
Flickr flickr = new Flickr();
flickr.ApiKey = myApiKey;
~~~
or
~~~
Flickr flickr = new Flickr(myApiKey);
~~~

The simplest method (although it has the most parameters) is probably the PhotosSearch method, 
which is best used by passing in a PhotoSearchOptions instance:

~~~
var options = new PhotoSearchOptions { Tags = "colorful", PerPage = 20, Page = 1 };
PhotoCollection photos = flickr.PhotosSearch(options);

foreach(Photo photo in photos) 
{
  Console.WriteLine("Photo {0} has title {1}", photo.PhotoId, photo.Title);
}
~~~

## Photo Extras
One of the hardest things to understand initially is that not all properties are returned by Flickr, you have to explicity request them.  
For example the following code would be used to return the Tags and the LargeUrl for a selection of photos:
~~~
var options = new PhotoSearchOptions { 
  Tags = "colorful", 
  PerPage = 20, 
  Page = 1, 
  Extras = PhotoSearchExtras.LargeUrl | PhotoSearchExtras.Tags 
};

PhotoCollection photos = flickr.PhotosSearch(options);
// Each photos Tags and LargeUrl properties should now be set, 
// assuming that the photo has any tags, and is large enough to have a LargeUrl image available.
~~~


# Sample Applications

I've started a separate CodePlex project to host sample applications: 

https://github.com/samjudson/flickrnet-samples

View the sample app live on the web here:

http://wackylabs.azurewebsites.net/

# License

The project is licensed under both the LGPL 2.1 license, and the Apache 2.0 license. 
This gives you the flexibility to do pretty much anything you want with the code. Enjoy!

# Contact

You can contact me at via the People tab or post a discussion here on codeplex if you require further help.

See my Flickr homepage at http://www.flickr.com/photos/samjudson

