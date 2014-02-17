
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FlickrNet;

namespace FlickrNet
{

	// ReSharper disable UseObjectOrCollectionInitializer

	public partial class Flickr
	{

		#region flickr.auth.oauth.checkToken

		public OAuthAccessToken AuthOauthCheckToken(string oauthToken) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.auth.oauth.checkToken");
			dictionary.Add("oauth_token", oauthToken);
			return GetResponse<OAuthAccessToken>(dictionary);
		}
		#endregion

		#region flickr.activity.userComments

		public ActivityItemCollection ActivityUserComments(int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.activity.userComments");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<ActivityItemCollection>(dictionary);
		}

		public ActivityItemCollection ActivityUserComments() 
		{
			return ActivityUserComments(0, 0);
		}

		#endregion

		#region flickr.activity.userPhotos

		public ActivityItemCollection ActivityUserPhotos(string timeframe, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.activity.userPhotos");
			if (timeframe != null) dictionary.Add("timeframe", timeframe);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<ActivityItemCollection>(dictionary);
		}

		public ActivityItemCollection ActivityUserPhotos() 
		{
			return ActivityUserPhotos(null, 0, 0);
		}


		public ActivityItemCollection ActivityUserPhotos(int page, int perPage) 
		{
			return ActivityUserPhotos(null, page, perPage);
		}


		public ActivityItemCollection ActivityUserPhotos(string timeframe) 
		{
			return ActivityUserPhotos(timeframe, 0, 0);
		}

		#endregion

		#region flickr.blogs.getList

		public BlogCollection BlogsGetList() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.blogs.getList");
			return GetResponse<BlogCollection>(dictionary);
		}
		#endregion

		#region flickr.blogs.getServices

		public BlogServiceCollection BlogsGetServices() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.blogs.getServices");
			return GetResponse<BlogServiceCollection>(dictionary);
		}
		#endregion

		#region flickr.blogs.postPhoto

		public void BlogsPostPhoto(string photoId, string title, string description, int blogId, string blogPassword, string service) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.blogs.postPhoto");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			if (blogId != 0) dictionary.Add("blog_id", blogId.ToString(CultureInfo.InvariantCulture));
			if (blogPassword != null) dictionary.Add("blog_password", blogPassword);
			if (service != null) dictionary.Add("service", service);
			GetResponse<NoResponse>(dictionary);
		}

		public void BlogsPostPhoto(string photoId, string title, string description, int blogId) 
		{
			BlogsPostPhoto(photoId, title, description, blogId, null, null);
		}


		public void BlogsPostPhoto(string photoId, string title, string description, int blogId, string blogPassword) 
		{
			BlogsPostPhoto(photoId, title, description, blogId, blogPassword, null);
		}


		public void BlogsPostPhoto(string photoId, string title, string description, string service) 
		{
			BlogsPostPhoto(photoId, title, description, 0, null, service);
		}


		public void BlogsPostPhoto(string photoId, string title, string description, string blogPassword, string service) 
		{
			BlogsPostPhoto(photoId, title, description, 0, blogPassword, service);
		}

		#endregion

		#region flickr.cameras.getBrandModels

		public CameraCollection CamerasGetBrandModels(string brand) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.cameras.getBrandModels");
			dictionary.Add("brand", brand);
			return GetResponse<CameraCollection>(dictionary);
		}
		#endregion

		#region flickr.cameras.getBrands

		public BrandCollection CamerasGetBrands() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.cameras.getBrands");
			return GetResponse<BrandCollection>(dictionary);
		}
		#endregion

		#region flickr.collections.getInfo

		public CollectionInfo CollectionsGetInfo(string collectionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.collections.getInfo");
			dictionary.Add("collection_id", collectionId);
			return GetResponse<CollectionInfo>(dictionary);
		}
		#endregion

		#region flickr.collections.getTree

		public CollectionCollection CollectionsGetTree(string collectionId, string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.collections.getTree");
			if (collectionId != null) dictionary.Add("collection_id", collectionId);
			if (userId != null) dictionary.Add("user_id", userId);
			return GetResponse<CollectionCollection>(dictionary);
		}

		public CollectionCollection CollectionsGetTree(string collectionId) 
		{
			return CollectionsGetTree(collectionId, null);
		}


		public CollectionCollection CollectionsGetTree() 
		{
			return CollectionsGetTree(null, null);
		}

		#endregion

		#region flickr.commons.getInstitutions

		public InstitutionCollection CommonsGetInstitutions() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.commons.getInstitutions");
			return GetResponse<InstitutionCollection>(dictionary);
		}
		#endregion

		#region flickr.contacts.getList

		public ContactCollection ContactsGetList(string filter, int page, int perPage, string sort) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getList");
			if (filter != null) dictionary.Add("filter", filter);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (sort != null) dictionary.Add("sort", sort);
			return GetResponse<ContactCollection>(dictionary);
		}

		public ContactCollection ContactsGetList() 
		{
			return ContactsGetList(null, 0, 0, null);
		}


		public ContactCollection ContactsGetList(int page, int perPage) 
		{
			return ContactsGetList(null, page, perPage, null);
		}


		public ContactCollection ContactsGetList(int page, int perPage, string sort) 
		{
			return ContactsGetList(null, page, perPage, sort);
		}


		public ContactCollection ContactsGetList(string filter) 
		{
			return ContactsGetList(filter, 0, 0, null);
		}


		public ContactCollection ContactsGetList(string filter, string sort) 
		{
			return ContactsGetList(filter, 0, 0, sort);
		}


		public ContactCollection ContactsGetList(string filter, int page, int perPage) 
		{
			return ContactsGetList(filter, page, perPage, null);
		}

		#endregion

		#region flickr.contacts.getListRecentlyUploaded

		public ContactCollection ContactsGetListRecentlyUploaded(DateTime? dateLastupdated, string filter) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getListRecentlyUploaded");
			if (dateLastupdated != null) dictionary.Add("date_lastupdated", dateLastupdated.Value.ToUnixTimestamp());
			if (filter != null) dictionary.Add("filter", filter);
			return GetResponse<ContactCollection>(dictionary);
		}

		public ContactCollection ContactsGetListRecentlyUploaded(DateTime? dateLastupdated) 
		{
			return ContactsGetListRecentlyUploaded(dateLastupdated, null);
		}


		public ContactCollection ContactsGetListRecentlyUploaded(string filter) 
		{
			return ContactsGetListRecentlyUploaded(null, filter);
		}


		public ContactCollection ContactsGetListRecentlyUploaded() 
		{
			return ContactsGetListRecentlyUploaded(null, null);
		}

		#endregion

		#region flickr.contacts.getPublicList

		public ContactCollection ContactsGetPublicList(string userId, int perPage, int page) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getPublicList");
			dictionary.Add("user_id", userId);
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			return GetResponse<ContactCollection>(dictionary);
		}

		public ContactCollection ContactsGetPublicList(string userId) 
		{
			return ContactsGetPublicList(userId, 0, 0);
		}

		#endregion

		#region flickr.contacts.getTaggingSuggestions

		public ContactCollection ContactsGetTaggingSuggestions(int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getTaggingSuggestions");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<ContactCollection>(dictionary);
		}

		public ContactCollection ContactsGetTaggingSuggestions() 
		{
			return ContactsGetTaggingSuggestions(0, 0);
		}

		#endregion

		#region flickr.favorites.add

		public void FavoritesAdd(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.add");
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.favorites.getContext

		public FavoriteContext FavoritesGetContext(string photoId, string userId, int numPrev, int numNext, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.getContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			dictionary.Add("num_prev", numPrev.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("num_next", numNext.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<FavoriteContext>(dictionary);
		}

		public FavoriteContext FavoritesGetContext(string photoId, string userId) 
		{
			return FavoritesGetContext(photoId, userId, 1, 1, PhotoSearchExtras.None);
		}


		public FavoriteContext FavoritesGetContext(string photoId, string userId, PhotoSearchExtras extras) 
		{
			return FavoritesGetContext(photoId, userId, 1, 1, extras);
		}


		public FavoriteContext FavoritesGetContext(string photoId, string userId, int numPrev, int numNext) 
		{
			return FavoritesGetContext(photoId, userId, numPrev, numNext, PhotoSearchExtras.None);
		}

		#endregion

		#region flickr.favorites.getList

		public PhotoCollection FavoritesGetList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.getList");
			if (userId != null) dictionary.Add("user_id", userId);
			if (minFaveDate != null) dictionary.Add("min_fave_date", minFaveDate.Value.ToUnixTimestamp());
			if (maxFaveDate != null) dictionary.Add("max_fave_date", maxFaveDate.Value.ToUnixTimestamp());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection FavoritesGetList() 
		{
			return FavoritesGetList(null, null, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection FavoritesGetList(DateTime? minFaveDate, DateTime? maxFaveDate) 
		{
			return FavoritesGetList(null, minFaveDate, maxFaveDate, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection FavoritesGetList(string userId) 
		{
			return FavoritesGetList(userId, null, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection FavoritesGetList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate) 
		{
			return FavoritesGetList(userId, minFaveDate, maxFaveDate, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection FavoritesGetList(PhotoSearchExtras extras) 
		{
			return FavoritesGetList(null, null, null, extras, 0, 0);
		}


		public PhotoCollection FavoritesGetList(DateTime? minFaveDate, DateTime? maxFaveDate, PhotoSearchExtras extras) 
		{
			return FavoritesGetList(null, minFaveDate, maxFaveDate, extras, 0, 0);
		}


		public PhotoCollection FavoritesGetList(string userId, PhotoSearchExtras extras) 
		{
			return FavoritesGetList(userId, null, null, extras, 0, 0);
		}


		public PhotoCollection FavoritesGetList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate, PhotoSearchExtras extras) 
		{
			return FavoritesGetList(userId, minFaveDate, maxFaveDate, extras, 0, 0);
		}


		public PhotoCollection FavoritesGetList(int page, int perPage) 
		{
			return FavoritesGetList(null, null, null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection FavoritesGetList(DateTime? minFaveDate, DateTime? maxFaveDate, int page, int perPage) 
		{
			return FavoritesGetList(null, minFaveDate, maxFaveDate, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection FavoritesGetList(string userId, int page, int perPage) 
		{
			return FavoritesGetList(userId, null, null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection FavoritesGetList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate, int page, int perPage) 
		{
			return FavoritesGetList(userId, minFaveDate, maxFaveDate, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection FavoritesGetList(PhotoSearchExtras extras, int page, int perPage) 
		{
			return FavoritesGetList(null, null, null, extras, page, perPage);
		}


		public PhotoCollection FavoritesGetList(DateTime? minFaveDate, DateTime? maxFaveDate, PhotoSearchExtras extras, int page, int perPage) 
		{
			return FavoritesGetList(null, minFaveDate, maxFaveDate, extras, page, perPage);
		}


		public PhotoCollection FavoritesGetList(string userId, PhotoSearchExtras extras, int page, int perPage) 
		{
			return FavoritesGetList(userId, null, null, extras, page, perPage);
		}

		#endregion

		#region flickr.favorites.getPublicList

		public PhotoCollection FavoritesGetPublicList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.getPublicList");
			dictionary.Add("user_id", userId);
			if (minFaveDate != null) dictionary.Add("min_fave_date", minFaveDate.Value.ToUnixTimestamp());
			if (maxFaveDate != null) dictionary.Add("max_fave_date", maxFaveDate.Value.ToUnixTimestamp());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection FavoritesGetPublicList(string userId) 
		{
			return FavoritesGetPublicList(userId, null, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection FavoritesGetPublicList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate) 
		{
			return FavoritesGetPublicList(userId, minFaveDate, maxFaveDate, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection FavoritesGetPublicList(string userId, PhotoSearchExtras extras) 
		{
			return FavoritesGetPublicList(userId, null, null, extras, 0, 0);
		}


		public PhotoCollection FavoritesGetPublicList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate, PhotoSearchExtras extras) 
		{
			return FavoritesGetPublicList(userId, minFaveDate, maxFaveDate, extras, 0, 0);
		}


		public PhotoCollection FavoritesGetPublicList(string userId, int page, int perPage) 
		{
			return FavoritesGetPublicList(userId, null, null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection FavoritesGetPublicList(string userId, DateTime? minFaveDate, DateTime? maxFaveDate, int page, int perPage) 
		{
			return FavoritesGetPublicList(userId, minFaveDate, maxFaveDate, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection FavoritesGetPublicList(string userId, PhotoSearchExtras extras, int page, int perPage) 
		{
			return FavoritesGetPublicList(userId, null, null, extras, page, perPage);
		}

		#endregion

		#region flickr.favorites.remove

		public void FavoritesRemove(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.remove");
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.galleries.addPhoto

		public void GalleriesAddPhoto(string galleryId, string photoId, string comment) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.addPhoto");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("photo_id", photoId);
			if (comment != null) dictionary.Add("comment", comment);
			GetResponse<NoResponse>(dictionary);
		}

		public void GalleriesAddPhoto(string galleryId, string photoId) 
		{
			GalleriesAddPhoto(galleryId, photoId, null);
		}

		#endregion

		#region flickr.galleries.create

		public void GalleriesCreate(string title, string description, string primaryPhotoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.create");
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			if (primaryPhotoId != null) dictionary.Add("primary_photo_id", primaryPhotoId);
			GetResponse<NoResponse>(dictionary);
		}

		public void GalleriesCreate(string title, string description) 
		{
			GalleriesCreate(title, description, null);
		}

		#endregion

		#region flickr.galleries.editMeta

		public void GalleriesEditMeta(string galleryId, string title, string description) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.editMeta");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("title", title);
			if (description != null) dictionary.Add("description", description);
			GetResponse<NoResponse>(dictionary);
		}

		public void GalleriesEditMeta(string galleryId, string title) 
		{
			GalleriesEditMeta(galleryId, title, null);
		}

		#endregion

		#region flickr.galleries.editPhoto

		public void GalleriesEditPhoto(string galleryId, string photoId, string comment) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.editPhoto");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("photo_id", photoId);
			dictionary.Add("comment", comment);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.galleries.editPhotos

		public void GalleriesEditPhotos(string galleryId, string primaryPhotoId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.editPhotos");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("primary_photo_id", primaryPhotoId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.galleries.getInfo

		public Gallery GalleriesGetInfo(string galleryId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getInfo");
			dictionary.Add("gallery_id", galleryId);
			return GetResponse<Gallery>(dictionary);
		}
		#endregion

		#region flickr.galleries.getList

		public GalleryCollection GalleriesGetList(string userId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getList");
			if (userId != null) dictionary.Add("user_id", userId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<GalleryCollection>(dictionary);
		}

		public GalleryCollection GalleriesGetList() 
		{
			return GalleriesGetList(null, 0, 0);
		}


		public GalleryCollection GalleriesGetList(string userId) 
		{
			return GalleriesGetList(userId, 0, 0);
		}


		public GalleryCollection GalleriesGetList(int page, int perPage) 
		{
			return GalleriesGetList(null, page, perPage);
		}

		#endregion

		#region flickr.galleries.getListForPhoto

		public GalleryCollection GalleriesGetListForPhoto(string photoId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getListForPhoto");
			dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<GalleryCollection>(dictionary);
		}

		public GalleryCollection GalleriesGetListForPhoto(string photoId) 
		{
			return GalleriesGetListForPhoto(photoId, 0, 0);
		}

		#endregion

		#region flickr.galleries.getPhotos

		public GalleryPhotoCollection GalleriesGetPhotos(string galleryId, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getPhotos");
			dictionary.Add("gallery_id", galleryId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<GalleryPhotoCollection>(dictionary);
		}

		public GalleryPhotoCollection GalleriesGetPhotos(string galleryId) 
		{
			return GalleriesGetPhotos(galleryId, PhotoSearchExtras.None);
		}

		#endregion

		#region flickr.groups.browse

		public GroupCategory GroupsBrowse(string catId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.browse");
			if (catId != null) dictionary.Add("cat_id", catId);
			return GetResponse<GroupCategory>(dictionary);
		}

		public GroupCategory GroupsBrowse() 
		{
			return GroupsBrowse(null);
		}

		#endregion

		#region flickr.groups.discuss.replies.add

		public void GroupsDiscussRepliesAdd(string topicId, string message) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.add");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("message", message);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.delete

		public void GroupsDiscussRepliesDelete(string topicId, string replyId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.delete");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("reply_id", replyId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.edit

		public void GroupsDiscussRepliesEdit(string topicId, string replyId, string message) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.edit");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("reply_id", replyId);
			dictionary.Add("message", message);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.getInfo

		public TopicReply GroupsDiscussRepliesGetInfo(string topicId, string replyId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.getInfo");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("reply_id", replyId);
			return GetResponse<TopicReply>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.getList

		public TopicReplyCollection GroupsDiscussRepliesGetList(string topicId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.getList");
			dictionary.Add("topic_id", topicId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<TopicReplyCollection>(dictionary);
		}

		public TopicReplyCollection GroupsDiscussRepliesGetList(string topicId) 
		{
			return GroupsDiscussRepliesGetList(topicId, 0, 0);
		}

		#endregion

		#region flickr.groups.discuss.topics.add

		public void GroupsDiscussTopicsAdd(string groupId, string subject, string message) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.topics.add");
			dictionary.Add("group_id", groupId);
			dictionary.Add("subject", subject);
			dictionary.Add("message", message);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.topics.getInfo

		public Topic GroupsDiscussTopicsGetInfo(string topicId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.topics.getInfo");
			dictionary.Add("topic_id", topicId);
			return GetResponse<Topic>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.topics.getList

		public TopicCollection GroupsDiscussTopicsGetList(string groupId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.topics.getList");
			dictionary.Add("group_id", groupId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<TopicCollection>(dictionary);
		}

		public TopicCollection GroupsDiscussTopicsGetList(string groupId) 
		{
			return GroupsDiscussTopicsGetList(groupId, 0, 0);
		}

		#endregion

		#region flickr.groups.getInfo

		public GroupFullInfo GroupsGetInfo(string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.getInfo");
			dictionary.Add("group_id", groupId);
			return GetResponse<GroupFullInfo>(dictionary);
		}
		#endregion

		#region flickr.groups.join

		public void GroupsJoin(string groupId, bool acceptRules) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.join");
			dictionary.Add("group_id", groupId);
			dictionary.Add("accept_rules", acceptRules ? "1" : "0");
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.join.request

		public void GroupsJoinRequest(string groupId, string message, bool acceptRules) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.join.request");
			dictionary.Add("group_id", groupId);
			dictionary.Add("message", message);
			dictionary.Add("accept_rules", acceptRules ? "1" : "0");
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.leave

		public void GroupsLeave(string groupId, bool? deletePhotos) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.leave");
			dictionary.Add("group_id", groupId);
			if (deletePhotos != null) dictionary.Add("delete_photos", deletePhotos.Value ? "1" : "0");
			GetResponse<NoResponse>(dictionary);
		}

		public void GroupsLeave(string groupId) 
		{
			GroupsLeave(groupId, null);
		}

		#endregion

		#region flickr.groups.members.getList

		public MemberCollection GroupsMembersGetList(string groupId, int page, int perPage, MemberTypes memberType) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.members.getList");
			dictionary.Add("group_id", groupId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (memberType != MemberTypes.None) dictionary.Add("member_type", memberType.ToString().ToLower());
			return GetResponse<MemberCollection>(dictionary);
		}

		public MemberCollection GroupsMembersGetList(string groupId) 
		{
			return GroupsMembersGetList(groupId, 0, 0, MemberTypes.None);
		}


		public MemberCollection GroupsMembersGetList(string groupId, int page, int perPage) 
		{
			return GroupsMembersGetList(groupId, page, perPage, MemberTypes.None);
		}


		public MemberCollection GroupsMembersGetList(string groupId, MemberTypes memberType) 
		{
			return GroupsMembersGetList(groupId, 0, 0, memberType);
		}

		#endregion

		#region flickr.groups.pools.add

		public void GroupsPoolsAdd(string photoId, string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.add");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("group_id", groupId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.pools.getContext

		public Context GroupsPoolsGetContext(string photoId, string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.getContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("group_id", groupId);
			return GetResponse<Context>(dictionary);
		}
		#endregion

		#region flickr.groups.pools.getGroups

		public MemberGroupInfoCollection GroupsPoolsGetGroups(int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.getGroups");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<MemberGroupInfoCollection>(dictionary);
		}

		public MemberGroupInfoCollection GroupsPoolsGetGroups() 
		{
			return GroupsPoolsGetGroups(0, 0);
		}

		#endregion

		#region flickr.groups.pools.getPhotos

		public PhotoCollection GroupsPoolsGetPhotos(string groupId, string tags, string userId, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.getPhotos");
			dictionary.Add("group_id", groupId);
			if (tags != null) dictionary.Add("tags", tags);
			if (userId != null) dictionary.Add("user_id", userId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection GroupsPoolsGetPhotos(string groupId) 
		{
			return GroupsPoolsGetPhotos(groupId, null, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, string tags) 
		{
			return GroupsPoolsGetPhotos(groupId, tags, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, string tags, PhotoSearchExtras extras) 
		{
			return GroupsPoolsGetPhotos(groupId, tags, null, extras, 0, 0);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, string tags, int page, int perPage) 
		{
			return GroupsPoolsGetPhotos(groupId, tags, null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, string tags, PhotoSearchExtras extras, int page, int perPage) 
		{
			return GroupsPoolsGetPhotos(groupId, tags, null, extras, page, perPage);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, int page, int perPage) 
		{
			return GroupsPoolsGetPhotos(groupId, null, null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, PhotoSearchExtras extras) 
		{
			return GroupsPoolsGetPhotos(groupId, null, null, extras, 0, 0);
		}


		public PhotoCollection GroupsPoolsGetPhotos(string groupId, PhotoSearchExtras extras, int page, int perPage) 
		{
			return GroupsPoolsGetPhotos(groupId, null, null, extras, page, perPage);
		}

		#endregion

		#region flickr.groups.pools.remove

		public void GroupsPoolsRemove(string photoId, string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.remove");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("group_id", groupId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.groups.search

		public GroupSearchResultCollection GroupsSearch(string text, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.search");
			dictionary.Add("text", text);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<GroupSearchResultCollection>(dictionary);
		}

		public GroupSearchResultCollection GroupsSearch(string text) 
		{
			return GroupsSearch(text, 0, 0);
		}

		#endregion

		#region flickr.interestingness.getList

		public PhotoCollection InterestingnessGetList(DateTime? date, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.interestingness.getList");
			if (date != null) dictionary.Add("date", date.Value.ToMySql());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection InterestingnessGetList() 
		{
			return InterestingnessGetList(null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection InterestingnessGetList(PhotoSearchExtras extras) 
		{
			return InterestingnessGetList(null, extras, 0, 0);
		}


		public PhotoCollection InterestingnessGetList(int page, int perPage) 
		{
			return InterestingnessGetList(null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection InterestingnessGetList(PhotoSearchExtras extras, int page, int perPage) 
		{
			return InterestingnessGetList(null, extras, page, perPage);
		}


		public PhotoCollection InterestingnessGetList(DateTime? date) 
		{
			return InterestingnessGetList(date, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection InterestingnessGetList(DateTime? date, int page, int perPage) 
		{
			return InterestingnessGetList(date, PhotoSearchExtras.None, page, perPage);
		}

		#endregion

		#region flickr.machinetags.getNamespaces

		public NamespaceCollection MachinetagsGetNamespaces(string predicate, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getNamespaces");
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<NamespaceCollection>(dictionary);
		}

		public NamespaceCollection MachinetagsGetNamespaces(string predicate) 
		{
			return MachinetagsGetNamespaces(predicate, 0, 1);
		}


		public NamespaceCollection MachinetagsGetNamespaces() 
		{
			return MachinetagsGetNamespaces(null, 0, 1);
		}

		#endregion

		#region flickr.machinetags.getPairs

		public PairCollection MachinetagsGetPairs(string namespaceName, string predicate, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getPairs");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PairCollection>(dictionary);
		}

		public PairCollection MachinetagsGetPairs() 
		{
			return MachinetagsGetPairs(null, null, 0, 1);
		}


		public PairCollection MachinetagsGetPairs(string namespaceName, string predicate) 
		{
			return MachinetagsGetPairs(namespaceName, predicate, 0, 1);
		}


		public PairCollection MachinetagsGetPairs(int page, int perPage) 
		{
			return MachinetagsGetPairs(null, null, page, perPage);
		}

		#endregion

		#region flickr.machinetags.getPredicates

		public PredicateCollection MachinetagsGetPredicates(string namespaceName, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getPredicates");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PredicateCollection>(dictionary);
		}

		public PredicateCollection MachinetagsGetPredicates() 
		{
			return MachinetagsGetPredicates(null, 0, 1);
		}


		public PredicateCollection MachinetagsGetPredicates(string namespaceName) 
		{
			return MachinetagsGetPredicates(namespaceName, 0, 1);
		}


		public PredicateCollection MachinetagsGetPredicates(int page, int perPage) 
		{
			return MachinetagsGetPredicates(null, page, perPage);
		}

		#endregion

		#region flickr.machinetags.getRecentValues

		public ValueCollection MachinetagsGetRecentValues(string namespaceName, string predicate, DateTime? addedSince, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getRecentValues");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (addedSince != null) dictionary.Add("added_since", addedSince.Value.ToUnixTimestamp());
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<ValueCollection>(dictionary);
		}

		public ValueCollection MachinetagsGetRecentValues(DateTime? addedSince) 
		{
			return MachinetagsGetRecentValues(null, null, addedSince, 0, 1);
		}


		public ValueCollection MachinetagsGetRecentValues(DateTime? addedSince, int page, int perPage) 
		{
			return MachinetagsGetRecentValues(null, null, addedSince, page, perPage);
		}


		public ValueCollection MachinetagsGetRecentValues(string namespaceName, string predicate) 
		{
			return MachinetagsGetRecentValues(namespaceName, predicate, null, 0, 1);
		}


		public ValueCollection MachinetagsGetRecentValues(string namespaceName, string predicate, int page, int perPage) 
		{
			return MachinetagsGetRecentValues(namespaceName, predicate, null, page, perPage);
		}

		#endregion

		#region flickr.machinetags.getValues

		public ValueCollection MachinetagsGetValues(string namespaceName, string predicate, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getValues");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<ValueCollection>(dictionary);
		}

		public ValueCollection MachinetagsGetValues(string namespaceName, string predicate) 
		{
			return MachinetagsGetValues(namespaceName, predicate, 0, 1);
		}

		#endregion

		#region flickr.panda.getList

		public PandaCollection PandaGetList() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.panda.getList");
			return GetResponse<PandaCollection>(dictionary);
		}
		#endregion

		#region flickr.panda.getPhotos

		public PandaPhotoCollection PandaGetPhotos(string pandaName, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.panda.getPhotos");
			dictionary.Add("panda_name", pandaName);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PandaPhotoCollection>(dictionary);
		}

		public PandaPhotoCollection PandaGetPhotos(string pandaName) 
		{
			return PandaGetPhotos(pandaName, PhotoSearchExtras.None, 0, 0);
		}


		public PandaPhotoCollection PandaGetPhotos(string pandaName, PhotoSearchExtras extras) 
		{
			return PandaGetPhotos(pandaName, extras, 0, 0);
		}


		public PandaPhotoCollection PandaGetPhotos(string pandaName, int page, int perPage) 
		{
			return PandaGetPhotos(pandaName, PhotoSearchExtras.None, page, perPage);
		}

		#endregion

		#region flickr.people.findByEmail

		public FoundUser PeopleFindByEmail(string findEmail) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.findByEmail");
			dictionary.Add("find_email", findEmail);
			return GetResponse<FoundUser>(dictionary);
		}
		#endregion

		#region flickr.people.findByUserName

		public FoundUser PeopleFindByUserName(string username) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.findByUserName");
			dictionary.Add("username", username);
			return GetResponse<FoundUser>(dictionary);
		}
		#endregion

		#region flickr.people.getGroups

		public GroupInfoCollection PeopleGetGroups(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getGroups");
			dictionary.Add("user_id", userId);
			return GetResponse<GroupInfoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.getInfo

		public Person PeopleGetInfo(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getInfo");
			dictionary.Add("user_id", userId);
			return GetResponse<Person>(dictionary);
		}
		#endregion

		#region flickr.people.getLimits

		public PersonLimits PeopleGetLimits() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getLimits");
			return GetResponse<PersonLimits>(dictionary);
		}
		#endregion

		#region flickr.people.getPhotos

		public PhotoCollection PeopleGetPhotos(string userId, SafetyLevel safeSearch, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate, ContentTypeSearch contentType, PrivacyFilter privacyFilter, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPhotos");
			dictionary.Add("user_id", userId);
			if (safeSearch != SafetyLevel.None) dictionary.Add("safe_search", safeSearch.ToString("d"));
			if (minUploadDate != null) dictionary.Add("min_upload_date", minUploadDate.Value.ToUnixTimestamp());
			if (maxUploadDate != null) dictionary.Add("max_upload_date", maxUploadDate.Value.ToUnixTimestamp());
			if (minTakenDate != null) dictionary.Add("min_taken_date", minTakenDate.Value.ToMySql());
			if (maxTakenDate != null) dictionary.Add("max_taken_date", maxTakenDate.Value.ToMySql());
			if (contentType != ContentTypeSearch.None) dictionary.Add("content_type", contentType.ToString("d"));
			if (privacyFilter != PrivacyFilter.None) dictionary.Add("privacy_filter", privacyFilter.ToString("d"));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PeopleGetPhotos() 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(string userId) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(PhotoSearchExtras extras) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(int page, int perPage) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(PhotoSearchExtras extras, int page, int perPage) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(string userId, int page, int perPage) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(string userId, PhotoSearchExtras extras, int page, int perPage) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, null, null, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(DateTime? minUploadDate, DateTime? maxUploadDate, PhotoSearchExtras extras) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(DateTime? minUploadDate, DateTime? maxUploadDate, PhotoSearchExtras extras, int page, int perPage) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(DateTime? minUploadDate, DateTime? maxUploadDate, int page, int perPage) 
		{
			return PeopleGetPhotos("me", SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(string userId, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(string userId, DateTime? minUploadDate, DateTime? maxUploadDate, PhotoSearchExtras extras) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0);
		}


		public PhotoCollection PeopleGetPhotos(string userId, DateTime? minUploadDate, DateTime? maxUploadDate, int page, int perPage) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PeopleGetPhotos(string userId, DateTime? minUploadDate, DateTime? maxUploadDate, PhotoSearchExtras extras, int page, int perPage) 
		{
			return PeopleGetPhotos(userId, SafetyLevel.None, minUploadDate, maxUploadDate, null, null, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage);
		}

		#endregion

		#region flickr.people.getPhotosOf

		public PeoplePhotoCollection PeopleGetPhotosOf(string userId, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPhotosOf");
			dictionary.Add("user_id", userId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PeoplePhotoCollection>(dictionary);
		}

		public PeoplePhotoCollection PeopleGetPhotosOf() 
		{
			return PeopleGetPhotosOf("me", PhotoSearchExtras.None, 0, 0);
		}


		public PeoplePhotoCollection PeopleGetPhotosOf(string userId) 
		{
			return PeopleGetPhotosOf(userId, PhotoSearchExtras.None, 0, 0);
		}


		public PeoplePhotoCollection PeopleGetPhotosOf(string userId, PhotoSearchExtras extras) 
		{
			return PeopleGetPhotosOf(userId, extras, 0, 0);
		}


		public PeoplePhotoCollection PeopleGetPhotosOf(string userId, int page, int perPage) 
		{
			return PeopleGetPhotosOf(userId, PhotoSearchExtras.None, page, perPage);
		}


		public PeoplePhotoCollection PeopleGetPhotosOf(PhotoSearchExtras extras, int page, int perPage) 
		{
			return PeopleGetPhotosOf("me", extras, page, perPage);
		}


		public PeoplePhotoCollection PeopleGetPhotosOf(int page, int perPage) 
		{
			return PeopleGetPhotosOf("me", PhotoSearchExtras.None, page, perPage);
		}

		#endregion

		#region flickr.people.getPublicGroups

		public GroupInfoCollection PeopleGetPublicGroups(string userId, bool? includeInvitationOnly) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPublicGroups");
			dictionary.Add("user_id", userId);
			if (includeInvitationOnly != null) dictionary.Add("include_invitation_only", includeInvitationOnly.Value ? "1" : "0");
			return GetResponse<GroupInfoCollection>(dictionary);
		}

		public GroupInfoCollection PeopleGetPublicGroups(string userId) 
		{
			return PeopleGetPublicGroups(userId, null);
		}

		#endregion

		#region flickr.people.getPublicPhotos

		public PhotoCollection PeopleGetPublicPhotos(string userId, int page, int perPage, SafetyLevel safetyLevel, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPublicPhotos");
			dictionary.Add("user_id", userId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (safetyLevel != SafetyLevel.None) dictionary.Add("safety_level", safetyLevel.ToString("d"));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PeopleGetPublicPhotos(string userId) 
		{
			return PeopleGetPublicPhotos(userId, 0, 0, SafetyLevel.None, PhotoSearchExtras.None);
		}


		public PhotoCollection PeopleGetPublicPhotos(string userId, int page, int perPage) 
		{
			return PeopleGetPublicPhotos(userId, page, perPage, SafetyLevel.None, PhotoSearchExtras.None);
		}


		public PhotoCollection PeopleGetPublicPhotos(string userId, int page, int perPage, PhotoSearchExtras extras) 
		{
			return PeopleGetPublicPhotos(userId, page, perPage, SafetyLevel.None, extras);
		}


		public PhotoCollection PeopleGetPublicPhotos(string userId, SafetyLevel safetyLevel) 
		{
			return PeopleGetPublicPhotos(userId, 0, 0, safetyLevel, PhotoSearchExtras.None);
		}


		public PhotoCollection PeopleGetPublicPhotos(string userId, SafetyLevel safetyLevel, PhotoSearchExtras extras) 
		{
			return PeopleGetPublicPhotos(userId, 0, 0, safetyLevel, extras);
		}


		public PhotoCollection PeopleGetPublicPhotos(string userId, int page, int perPage, SafetyLevel safetyLevel) 
		{
			return PeopleGetPublicPhotos(userId, page, perPage, safetyLevel, PhotoSearchExtras.None);
		}

		#endregion

		#region flickr.people.getUploadStatus

		public UserStatus PeopleGetUploadStatus() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getUploadStatus");
			return GetResponse<UserStatus>(dictionary);
		}
		#endregion

		#region flickr.photos.addTags

		public void PhotosAddTags(string photoId, IEnumerable<string> tags) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.addTags");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("tags", tags == null ? String.Empty : String.Join(",", tags.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.comments.addComment

		public string PhotosCommentsAddComment(string photoId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.addComment");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("comment_text", commentText);
			var result = GetResponse<UnknownResponse>(dictionary);
			return result.GetAttributeValue<string>("comment", "id");
		}
		#endregion

		#region flickr.photos.comments.deleteComment

		public void PhotosCommentsDeleteComment(string commentId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.deleteComment");
			dictionary.Add("comment_id", commentId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.comments.editComment

		public void PhotosCommentsEditComment(string commentId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.editComment");
			dictionary.Add("comment_id", commentId);
			dictionary.Add("comment_text", commentText);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.comments.getList

		public PhotoCommentCollection PhotosCommentsGetList(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.getList");
			dictionary.Add("photo_id", photoId);
			return GetResponse<PhotoCommentCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.comments.getRecentForContacts

		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, IEnumerable<string> contactsFilter, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.getRecentForContacts");
			if (dateLastComment != null) dictionary.Add("date_last_comment", dateLastComment.Value.ToUnixTimestamp());
			if (contactsFilter != null) dictionary.Add("contacts_filter", contactsFilter == null ? String.Empty : String.Join(",", contactsFilter.ToArray()));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosCommentsGetRecentForContacts() 
		{
			return PhotosCommentsGetRecentForContacts(null, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, null, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, IEnumerable<string> contactsFilter) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, contactsFilter, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, IEnumerable<string> contactsFilter, PhotoSearchExtras extras) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, contactsFilter, extras, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, IEnumerable<string> contactsFilter, int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, contactsFilter, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, PhotoSearchExtras extras) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, null, extras, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, PhotoSearchExtras extras, int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, null, extras, page, perPage);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(DateTime? dateLastComment, int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(dateLastComment, null, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(IEnumerable<string> contactsFilter) 
		{
			return PhotosCommentsGetRecentForContacts(null, contactsFilter, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(IEnumerable<string> contactsFilter, PhotoSearchExtras extras) 
		{
			return PhotosCommentsGetRecentForContacts(null, contactsFilter, extras, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(IEnumerable<string> contactsFilter, int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(null, contactsFilter, PhotoSearchExtras.None, page, perPage);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(IEnumerable<string> contactsFilter, PhotoSearchExtras extras, int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(null, contactsFilter, extras, page, perPage);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(PhotoSearchExtras extras) 
		{
			return PhotosCommentsGetRecentForContacts(null, null, extras, 0, 0);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(PhotoSearchExtras extras, int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(null, null, extras, page, perPage);
		}


		public PhotoCollection PhotosCommentsGetRecentForContacts(int page, int perPage) 
		{
			return PhotosCommentsGetRecentForContacts(null, null, PhotoSearchExtras.None, page, perPage);
		}

		#endregion

		#region flickr.photos.delete

		public void PhotosDelete(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.delete");
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.addPhoto

		public void PhotosetsAddPhoto(string photosetId, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.addPhoto");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.comments.addComment

		public string PhotosetsCommentsAddComment(string photosetId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.addComment");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("comment_text", commentText);
			var result = GetResponse<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.photosets.comments.deleteComment

		public void PhotosetsCommentsDeleteComment(string commentId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.deleteComment");
			dictionary.Add("comment_id", commentId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.comments.editComment

		public void PhotosetsCommentsEditComment(string commentId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.editComment");
			dictionary.Add("comment_id", commentId);
			dictionary.Add("comment_text", commentText);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.comments.getList

		public PhotosetCommentCollection PhotosetsCommentsGetList(string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.getList");
			dictionary.Add("photoset_id", photosetId);
			return GetResponse<PhotosetCommentCollection>(dictionary);
		}
		#endregion

		#region flickr.photosets.create

		public Photoset PhotosetsCreate(string title, string description, string primaryPhotoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.create");
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			if (primaryPhotoId != null) dictionary.Add("primary_photo_id", primaryPhotoId);
			return GetResponse<Photoset>(dictionary);
		}
		#endregion

		#region flickr.photosets.delete

		public void PhotosetsDelete(string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.delete");
			dictionary.Add("photoset_id", photosetId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.editMeta

		public void PhotosetsEditMeta(string photosetId, string title, string description) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.editMeta");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.editPhotos

		public void PhotosetsEditPhotos(string photosetId, string primaryPhotoId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.editPhotos");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("primary_photo_id", primaryPhotoId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.getContext

		public Context PhotosetsGetContext(string photoId, string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("photoset_id", photosetId);
			return GetResponse<Context>(dictionary);
		}
		#endregion

		#region flickr.photosets.getInfo

		public Photoset PhotosetsGetInfo(string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getInfo");
			dictionary.Add("photoset_id", photosetId);
			return GetResponse<Photoset>(dictionary);
		}
		#endregion

		#region flickr.photosets.getList

		public PhotosetCollection PhotosetsGetList(string userId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getList");
			if (userId != null) dictionary.Add("user_id", userId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotosetCollection>(dictionary);
		}

		public PhotosetCollection PhotosetsGetList() 
		{
			return PhotosetsGetList(null, 0, 0);
		}


		public PhotosetCollection PhotosetsGetList(int page, int perPage) 
		{
			return PhotosetsGetList(null, page, perPage);
		}


		public PhotosetCollection PhotosetsGetList(string userId) 
		{
			return PhotosetsGetList(userId, 0, 0);
		}

		#endregion

		#region flickr.photosets.getPhotos

		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter, int page, int perPage, MediaType media) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getPhotos");
			dictionary.Add("photoset_id", photosetId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (privacyFilter != PrivacyFilter.None) dictionary.Add("privacy_filter", privacyFilter.ToString("d"));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (media != MediaType.None) dictionary.Add("media", media.ToString().ToLower());
			return GetResponse<PhotosetPhotoCollection>(dictionary);
		}

		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId) 
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, PrivacyFilter.None, 0, 0, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras) 
		{
			return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None, 0, 0, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, int page, int perPage) 
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, PrivacyFilter.None, page, perPage, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, int page, int perPage) 
		{
			return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None, page, perPage, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter) 
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, privacyFilter, 0, 0, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter, int page, int perPage) 
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, privacyFilter, page, perPage, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter) 
		{
			return PhotosetsGetPhotos(photosetId, extras, privacyFilter, 0, 0, MediaType.None);
		}


		public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter, int page, int perPage) 
		{
			return PhotosetsGetPhotos(photosetId, extras, privacyFilter, page, perPage, MediaType.None);
		}

		#endregion

		#region flickr.photosets.orderSets

		public void PhotosetsOrderSets(IEnumerable<string> photosetIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.orderSets");
			dictionary.Add("photoset_ids", photosetIds == null ? String.Empty : String.Join(",", photosetIds.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.removePhoto

		public void PhotosetsRemovePhoto(string photosetId, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.removePhoto");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.removePhotos

		public void PhotosetsRemovePhotos(string photosetId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.removePhotos");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.reorderPhotos

		public void PhotosetsReorderPhotos(string photosetId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.reorderPhotos");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photosets.setPrimaryPhoto

		public void PhotosetsSetPrimaryPhoto(string photosetId, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.setPrimaryPhoto");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.batchCorrectLocation

		public void PhotosGeoBatchCorrectLocation(double latitude, double longitude, GeoAccuracy accuracy, string placeId, string woeId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.batchCorrectLocation");
			dictionary.Add("latitude", latitude.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("longitude", longitude.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			GetResponse<NoResponse>(dictionary);
		}

		public void PhotosGeoBatchCorrectLocation(double latitude, double longitude, string placeId, string woeId) 
		{
			PhotosGeoBatchCorrectLocation(latitude, longitude, GeoAccuracy.None, placeId, woeId);
		}

		#endregion

		#region flickr.photos.geo.correctLocation

		public void PhotosGeoCorrectLocation(string photoId, string placeId, string woeId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.correctLocation");
			dictionary.Add("photo_id", photoId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.getLocation

		public PlaceInfo PhotosGeoGetLocation(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.getLocation");
			dictionary.Add("photo_id", photoId);
			return GetResponse<PlaceInfo>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.getPerms

		public GeoPermissions PhotosGeoGetPerms(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.getPerms");
			dictionary.Add("photo_id", photoId);
			return GetResponse<GeoPermissions>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.photosForLocation

		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon, GeoAccuracy accuracy, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.photosForLocation");
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("perPage", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon) 
		{
			return PhotosGeoPhotosForLocation(lat, lon, GeoAccuracy.None, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon, int page) 
		{
			return PhotosGeoPhotosForLocation(lat, lon, GeoAccuracy.None, PhotoSearchExtras.None, page, 0);
		}


		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon, PhotoSearchExtras extras) 
		{
			return PhotosGeoPhotosForLocation(lat, lon, GeoAccuracy.None, extras, 0, 0);
		}


		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon, PhotoSearchExtras extras, int page) 
		{
			return PhotosGeoPhotosForLocation(lat, lon, GeoAccuracy.None, extras, page, 0);
		}


		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon, GeoAccuracy accuracy) 
		{
			return PhotosGeoPhotosForLocation(lat, lon, accuracy, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosGeoPhotosForLocation(double lat, double lon, GeoAccuracy accuracy, int page) 
		{
			return PhotosGeoPhotosForLocation(lat, lon, accuracy, PhotoSearchExtras.None, page, 0);
		}

		#endregion

		#region flickr.photos.geo.removeLocation

		public void PhotosGeoRemoveLocation(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.removeLocation");
			dictionary.Add("photo_id", photoId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.setContext

		public void PhotosGeoSetContext(string photoId, GeoContext context) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.setContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("context", context.ToString("d"));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.setLocation

		public void PhotosGeoSetLocation(string photoId, double lat, double lon, GeoAccuracy accuracy, GeoContext context) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.setLocation");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (context != GeoContext.NotDefined) dictionary.Add("context", context.ToString("d"));
			GetResponse<NoResponse>(dictionary);
		}

		public void PhotosGeoSetLocation(string photoId, double lat, double lon) 
		{
			PhotosGeoSetLocation(photoId, lat, lon, GeoAccuracy.None, GeoContext.NotDefined);
		}


		public void PhotosGeoSetLocation(string photoId, double lat, double lon, GeoAccuracy accuracy) 
		{
			PhotosGeoSetLocation(photoId, lat, lon, accuracy, GeoContext.NotDefined);
		}


		public void PhotosGeoSetLocation(string photoId, double lat, double lon, GeoContext context) 
		{
			PhotosGeoSetLocation(photoId, lat, lon, GeoAccuracy.None, context);
		}

		#endregion

		#region flickr.photos.geo.setPerms

		public void PhotosGeoSetPerms(string photoId, bool isPublic, bool isContact, bool isFamily, bool isFriend) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.setPerms");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("is_public", isPublic ? "1" : "0");
			dictionary.Add("is_contact", isContact ? "1" : "0");
			dictionary.Add("is_family", isFamily ? "1" : "0");
			dictionary.Add("is_friend", isFriend ? "1" : "0");
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.getAllContexts

		public AllContexts PhotosGetAllContexts(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getAllContexts");
			dictionary.Add("photo_id", photoId);
			return GetResponse<AllContexts>(dictionary);
		}
		#endregion

		#region flickr.photos.getContactsPhotos

		public PhotoCollection PhotosGetContactsPhotos(int? count, bool? justFriends, bool? singlePhoto, bool? includeSelf, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getContactsPhotos");
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			if (justFriends != null) dictionary.Add("just_friends", justFriends.Value ? "1" : "0");
			if (singlePhoto != null) dictionary.Add("single_photo", singlePhoto.Value ? "1" : "0");
			if (includeSelf != null) dictionary.Add("include_self", includeSelf.Value ? "1" : "0");
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetContactsPhotos() 
		{
			return PhotosGetContactsPhotos(null, null, null, null, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetContactsPhotos(int? count) 
		{
			return PhotosGetContactsPhotos(count, null, null, null, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetContactsPhotos(int? count, PhotoSearchExtras extras) 
		{
			return PhotosGetContactsPhotos(count, null, null, null, extras);
		}


		public PhotoCollection PhotosGetContactsPhotos(PhotoSearchExtras extras) 
		{
			return PhotosGetContactsPhotos(null, null, null, null, extras);
		}

		#endregion

		#region flickr.photos.getContactsPublicPhotos

		public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int? count, bool? justFriends, bool? singlePhoto, bool? includeSelf, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getContactsPublicPhotos");
			dictionary.Add("user_id", userId);
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			if (justFriends != null) dictionary.Add("just_friends", justFriends.Value ? "1" : "0");
			if (singlePhoto != null) dictionary.Add("single_photo", singlePhoto.Value ? "1" : "0");
			if (includeSelf != null) dictionary.Add("include_self", includeSelf.Value ? "1" : "0");
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetContactsPublicPhotos(string userId) 
		{
			return PhotosGetContactsPublicPhotos(userId, null, null, null, null, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int? count) 
		{
			return PhotosGetContactsPublicPhotos(userId, count, null, null, null, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int? count, bool? justFriends, bool? singlePhoto, bool? includeSelf) 
		{
			return PhotosGetContactsPublicPhotos(userId, count, justFriends, singlePhoto, includeSelf, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int? count, PhotoSearchExtras extras) 
		{
			return PhotosGetContactsPublicPhotos(userId, count, null, null, null, extras);
		}


		public PhotoCollection PhotosGetContactsPublicPhotos(string userId, PhotoSearchExtras extras) 
		{
			return PhotosGetContactsPublicPhotos(userId, null, null, null, null, extras);
		}

		#endregion

		#region flickr.photos.getContext

		public Context PhotosGetContext(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getContext");
			dictionary.Add("photo_id", photoId);
			return GetResponse<Context>(dictionary);
		}
		#endregion

		#region flickr.photos.getCounts

		public PhotoCountCollection PhotosGetCounts(IEnumerable<DateTime> dates, IEnumerable<DateTime> takenDates) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getCounts");
			if (dates != null) dictionary.Add("dates", dates == null ? String.Empty : String.Join(",", dates.Select(d => d.ToUnixTimestamp()).ToArray()));
			if (takenDates != null) dictionary.Add("taken_dates", takenDates == null ? String.Empty : String.Join(",", takenDates.Select(d => d.ToUnixTimestamp()).ToArray()));
			return GetResponse<PhotoCountCollection>(dictionary);
		}

		public PhotoCountCollection PhotosGetCounts(IEnumerable<DateTime> dates) 
		{
			return PhotosGetCounts(dates, null);
		}

		#endregion

		#region flickr.photos.getExif

		public ExifTagCollection PhotosGetExif(string photoId, string secret) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getExif");
			dictionary.Add("photo_id", photoId);
			if (secret != null) dictionary.Add("secret", secret);
			return GetResponse<ExifTagCollection>(dictionary);
		}

		public ExifTagCollection PhotosGetExif(string photoId) 
		{
			return PhotosGetExif(photoId, null);
		}

		#endregion

		#region flickr.photos.getFavorites

		public PhotoFavoriteCollection PhotosGetFavorites(string photoId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getFavorites");
			dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoFavoriteCollection>(dictionary);
		}

		public PhotoFavoriteCollection PhotosGetFavorites(string photoId) 
		{
			return PhotosGetFavorites(photoId, 0, 0);
		}

		#endregion

		#region flickr.photos.getInfo

		public PhotoInfo PhotosGetInfo(string photoId, string secret) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getInfo");
			dictionary.Add("photo_id", photoId);
			if (secret != null) dictionary.Add("secret", secret);
			return GetResponse<PhotoInfo>(dictionary);
		}

		public PhotoInfo PhotosGetInfo(string photoId) 
		{
			return PhotosGetInfo(photoId, null);
		}

		#endregion

		#region flickr.photos.getNotInSet

		public PhotoCollection PhotosGetNotInSet(int page, int perPage, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getNotInSet");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetNotInSet() 
		{
			return PhotosGetNotInSet(0, 0, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetNotInSet(PhotoSearchExtras extras) 
		{
			return PhotosGetNotInSet(0, 0, extras);
		}


		public PhotoCollection PhotosGetNotInSet(int page, int perPage) 
		{
			return PhotosGetNotInSet(page, perPage, PhotoSearchExtras.None);
		}

		#endregion

		#region flickr.photos.getNotInSet

		public PhotoCollection PhotosGetNotInSet(PartialSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getNotInSet");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return GetResponse<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getPerms

		public PhotoPermissions PhotosGetPerms(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getPerms");
			dictionary.Add("photo_id", photoId);
			return GetResponse<PhotoPermissions>(dictionary);
		}
		#endregion

		#region flickr.photos.getRecent

		public PhotoCollection PhotosGetRecent(int page, int perPage, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getRecent");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetRecent() 
		{
			return PhotosGetRecent(0, 0, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetRecent(PhotoSearchExtras extras) 
		{
			return PhotosGetRecent(0, 0, extras);
		}


		public PhotoCollection PhotosGetRecent(int page, int perPage) 
		{
			return PhotosGetRecent(page, perPage, PhotoSearchExtras.None);
		}

		#endregion

		#region flickr.photos.getSizes

		public SizeCollection PhotosGetSizes(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getSizes");
			dictionary.Add("photo_id", photoId);
			return GetResponse<SizeCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getUntagged

		public PhotoCollection PhotosGetUntagged(PartialSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getUntagged");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return GetResponse<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getUntagged

		public PhotoCollection PhotosGetUntagged(int page, int perPage, PhotoSearchExtras extras) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getUntagged");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetUntagged() 
		{
			return PhotosGetUntagged(0, 0, PhotoSearchExtras.None);
		}


		public PhotoCollection PhotosGetUntagged(PhotoSearchExtras extras) 
		{
			return PhotosGetUntagged(0, 0, extras);
		}


		public PhotoCollection PhotosGetUntagged(int page, int perPage) 
		{
			return PhotosGetUntagged(page, perPage, PhotoSearchExtras.None);
		}

		#endregion

		#region flickr.photos.getWithGeoData

		public PhotoCollection PhotosGetWithGeoData(PartialSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getWithGeoData");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetWithGeoData() 
		{
			return PhotosGetWithGeoData(null);
		}

		#endregion

		#region flickr.photos.getWithoutGeoData

		public PhotoCollection PhotosGetWithoutGeoData(PartialSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getWithoutGeoData");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosGetWithoutGeoData() 
		{
			return PhotosGetWithoutGeoData(null);
		}

		#endregion

		#region flickr.photos.licenses.setLicense

		public void PhotosLicensesSetLicense(string photoId, LicenseType licenseId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.licenses.setLicense");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("license_id", licenseId.ToString("d"));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.licenses.getInfo

		public LicenseCollection PhotosLicensesGetInfo() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.licenses.getInfo");
			return GetResponse<LicenseCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.notes.add

		public string PhotosNotesAdd(string photoId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.notes.add");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("note_x", noteX.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_y", noteY.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_width", noteWidth.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_height", noteHeight.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_text", noteText);
			var result = GetResponse<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.photos.notes.delete

		public void PhotosNotesDelete(string noteId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.notes.delete");
			dictionary.Add("note_id", noteId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.notes.edit

		public void PhotosNotesEdit(string noteId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.notes.edit");
			dictionary.Add("note_id", noteId);
			dictionary.Add("note_x", noteX.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_y", noteY.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_width", noteWidth.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_height", noteHeight.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_text", noteText);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.people.add

		public void PhotosPeopleAdd(string photoId, string userId, int? personX, int? personY, int? personWidth, int? personHeight) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.add");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			if (personX != null) dictionary.Add("person_x", personX.ToString().ToLower());
			if (personY != null) dictionary.Add("person_y", personY.ToString().ToLower());
			if (personWidth != null) dictionary.Add("person_width", personWidth.ToString().ToLower());
			if (personHeight != null) dictionary.Add("person_height", personHeight.ToString().ToLower());
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.people.delete

		public void PhotosPeopleDelete(string photoId, string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.delete");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.people.deleteCoords

		public void PhotosPeopleDeleteCoords(string photoId, string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.deleteCoords");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.people.editCoords

		public void PhotosPeopleEditCoords(string photoId, string userId, int? personX, int? personY, int? personWidth, int? personHeight) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.editCoords");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			if (personX != null) dictionary.Add("person_x", personX.ToString().ToLower());
			if (personY != null) dictionary.Add("person_y", personY.ToString().ToLower());
			if (personWidth != null) dictionary.Add("person_width", personWidth.ToString().ToLower());
			if (personHeight != null) dictionary.Add("person_height", personHeight.ToString().ToLower());
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.people.getList

		public PhotoPersonCollection PhotosPeopleGetList(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.getList");
			dictionary.Add("photo_id", photoId);
			return GetResponse<PhotoPersonCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.recentlyUpdated

		public PhotoCollection PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.recentlyUpdated");
			dictionary.Add("min_date", minDate.ToUnixTimestamp());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PhotoCollection>(dictionary);
		}

		public PhotoCollection PhotosRecentlyUpdated(DateTime minDate) 
		{
			return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, 0, 0);
		}


		public PhotoCollection PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras) 
		{
			return PhotosRecentlyUpdated(minDate, extras, 0, 0);
		}


		public PhotoCollection PhotosRecentlyUpdated(DateTime minDate, int page, int perPage) 
		{
			return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, page, perPage);
		}

		#endregion

		#region flickr.photos.removeTag

		public void PhotosRemoveTag(string tagId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.removeTag");
			dictionary.Add("tag_id", tagId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.search

		public PhotoCollection PhotosSearch(PhotoSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.search");
			options.AddToDictionary(dictionary);
			return GetResponse<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.setContentType

		public void PhotosSetContentType(string photoId, ContentType contentType) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setContentType");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("content_type", contentType.ToString().ToLower());
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.setDates

		public void PhotosSetDates(string photoId, DateTime? datePosted, DateTime? dateTaken, DateGranularity granularity) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setDates");
			dictionary.Add("photo_id", photoId);
			if (datePosted != null) dictionary.Add("date_posted", datePosted.Value.ToUnixTimestamp());
			if (dateTaken != null) dictionary.Add("date_taken", dateTaken.Value.ToUnixTimestamp());
			if (granularity != DateGranularity.FullDate) dictionary.Add("granularity", granularity.ToString().ToLower());
			GetResponse<NoResponse>(dictionary);
		}

		public void PhotosSetDates(string photoId, DateTime? datePosted) 
		{
			PhotosSetDates(photoId, datePosted, null, DateGranularity.FullDate);
		}


		public void PhotosSetDates(string photoId, DateTime? dateTaken, DateGranularity granularity) 
		{
			PhotosSetDates(photoId, null, dateTaken, granularity);
		}

		#endregion

		#region flickr.photos.setMeta

		public void PhotosSetMeta(string photoId, string title, string description) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setMeta");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.setPerms

		public void PhotosSetPerms(string photoId, bool isPublic, bool isFriend, bool isFamily, PermissionComment permComments, PermissionAddMeta permAddMeta) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setPerms");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("is_public", isPublic ? "1" : "0");
			dictionary.Add("is_friend", isFriend ? "1" : "0");
			dictionary.Add("is_family", isFamily ? "1" : "0");
			dictionary.Add("perm_comments", permComments.ToString().ToLower());
			dictionary.Add("perm_add_meta", permAddMeta.ToString().ToLower());
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.setSafetyLevel

		public void PhotosSetSafetyLevel(string photoId, SafetyLevel safetyLevel, HiddenFromSearch hidden) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setSafetyLevel");
			dictionary.Add("photo_id", photoId);
			if (safetyLevel != SafetyLevel.None) dictionary.Add("safety_level", safetyLevel.ToString("d"));
			if (hidden != HiddenFromSearch.None) dictionary.Add("hidden", hidden.ToString().ToLower());
			GetResponse<NoResponse>(dictionary);
		}

		public void PhotosSetSafetyLevel(string photoId, SafetyLevel safetyLevel) 
		{
			PhotosSetSafetyLevel(photoId, safetyLevel, HiddenFromSearch.None);
		}


		public void PhotosSetSafetyLevel(string photoId, HiddenFromSearch hidden) 
		{
			PhotosSetSafetyLevel(photoId, SafetyLevel.None, hidden);
		}

		#endregion

		#region flickr.photos.setTags

		public void PhotosSetTags(string photoId, IEnumerable<string> tags) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setTags");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("tags", tags == null ? String.Empty : String.Join(",", tags.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.approveSuggestion

		public void PhotosSuggestionsApproveSuggestion(string suggestionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.approveSuggestion");
			dictionary.Add("suggestion_id", suggestionId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.getList

		public SuggestionCollection PhotosSuggestionsGetList(string photoId, SuggestionStatus status) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.getList");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("status", status.ToString().ToLower());
			return GetResponse<SuggestionCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.rejectSuggestion

		public void PhotosSuggestionsRejectSuggestion(string suggestionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.rejectSuggestion");
			dictionary.Add("suggestion_id", suggestionId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.removeSuggestion

		public void PhotosSuggestionsRemoveSuggestion(string suggestionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.removeSuggestion");
			dictionary.Add("suggestion_id", suggestionId);
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.suggestLocation

		public void PhotosSuggestionsSuggestLocation(string photoId, double lat, double lon, GeoAccuracy accuracy, string woeId, string placeId, string note) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.suggestLocation");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (woeId != null) dictionary.Add("woe_id", woeId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (note != null) dictionary.Add("note", note);
			GetResponse<NoResponse>(dictionary);
		}

		public void PhotosSuggestionsSuggestLocation(string photoId, double lat, double lon) 
		{
			PhotosSuggestionsSuggestLocation(photoId, lat, lon, GeoAccuracy.None, null, null, null);
		}


		public void PhotosSuggestionsSuggestLocation(string photoId, double lat, double lon, GeoAccuracy accuracy) 
		{
			PhotosSuggestionsSuggestLocation(photoId, lat, lon, accuracy, null, null, null);
		}


		public void PhotosSuggestionsSuggestLocation(string photoId, double lat, double lon, GeoAccuracy accuracy, string woeId) 
		{
			PhotosSuggestionsSuggestLocation(photoId, lat, lon, accuracy, woeId, null, null);
		}


		public void PhotosSuggestionsSuggestLocation(string photoId, double lat, double lon, string woeId) 
		{
			PhotosSuggestionsSuggestLocation(photoId, lat, lon, GeoAccuracy.None, woeId, null, null);
		}

		#endregion

		#region flickr.photos.transformRotate

		public void PhotosTransformRotate(string photoId, int degrees) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.transformRotate");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("degrees", degrees.ToString(CultureInfo.InvariantCulture));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.photos.upload.checkTickets

		public TicketCollection PhotosUploadCheckTickets(IEnumerable<string> tickets) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.upload.checkTickets");
			dictionary.Add("tickets", tickets == null ? String.Empty : String.Join(",", tickets.ToArray()));
			return GetResponse<TicketCollection>(dictionary);
		}
		#endregion

		#region flickr.places.find

		public PlaceCollection PlacesFind(string query) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.find");
			dictionary.Add("query", query);
			return GetResponse<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.findByLatLon

		public PlaceCollection PlacesFindByLatLon(double lat, double lon, GeoAccuracy accuracy) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.findByLatLon");
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			return GetResponse<PlaceCollection>(dictionary);
		}

		public PlaceCollection PlacesFindByLatLon(double lat, double lon) 
		{
			return PlacesFindByLatLon(lat, lon, GeoAccuracy.None);
		}

		#endregion

		#region flickr.places.getChildrenWithPhotosPublic

		public PlaceCollection PlacesGetChildrenWithPhotosPublic(string placeId, string woeId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getChildrenWithPhotosPublic");
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return GetResponse<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getInfo

		public PlaceInfo PlacesGetInfo(string placeId, string woeId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getInfo");
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return GetResponse<PlaceInfo>(dictionary);
		}
		#endregion

		#region flickr.places.getInfoByUrl

		public PlaceInfo PlacesGetInfoByUrl(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getInfoByUrl");
			dictionary.Add("url", url);
			return GetResponse<PlaceInfo>(dictionary);
		}
		#endregion

		#region flickr.places.getPlaceTypes

		public PlaceTypeInfoCollection PlacesGetPlaceTypes() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getPlaceTypes");
			return GetResponse<PlaceTypeInfoCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getShapeHistory

		public ShapeDataCollection PlacesGetShapeHistory(string placeId, string woeId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getShapeHistory");
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return GetResponse<ShapeDataCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getTopPlacesList

		public PlaceCollection PlacesGetTopPlacesList(PlaceType placeTypeId, DateTime? date, string placeId, string woeId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getTopPlacesList");
			dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (date != null) dictionary.Add("date", date.Value.ToUnixTimestamp());
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return GetResponse<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.placesForBoundingBox

		public PlaceCollection PlacesPlacesForBoundingBox(BoundaryBox bbox, PlaceType placeType, int? placeTypeId, bool? recursive) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.placesForBoundingBox");
			dictionary.Add("bbox", bbox.ToString().ToLower());
			if (placeType != PlaceType.None) dictionary.Add("place_type", placeType.ToString().ToLower());
			if (placeTypeId != null) dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (recursive != null) dictionary.Add("recursive", recursive.Value ? "1" : "0");
			return GetResponse<PlaceCollection>(dictionary);
		}

		public PlaceCollection PlacesPlacesForBoundingBox(BoundaryBox bbox, PlaceType placeType) 
		{
			return PlacesPlacesForBoundingBox(bbox, placeType, null, null);
		}


		public PlaceCollection PlacesPlacesForBoundingBox(BoundaryBox bbox) 
		{
			return PlacesPlacesForBoundingBox(bbox, PlaceType.None, null, null);
		}

		#endregion

		#region flickr.places.placesForContacts

		public PlaceCollection PlacesPlacesForContacts(PlaceType placeType, int? placeTypeId, string placeId, string woeId, int? threshold, string contacts, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.placesForContacts");
			if (placeType != PlaceType.None) dictionary.Add("place_type", placeType.ToString().ToLower());
			if (placeTypeId != null) dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			if (threshold != null) dictionary.Add("threshold", threshold.ToString().ToLower());
			if (contacts != null) dictionary.Add("contacts", contacts);
			if (minUploadDate != null) dictionary.Add("min_upload_date", minUploadDate.Value.ToUnixTimestamp());
			if (maxUploadDate != null) dictionary.Add("max_upload_date", maxUploadDate.Value.ToUnixTimestamp());
			if (minTakenDate != null) dictionary.Add("min_taken_date", minTakenDate.Value.ToUnixTimestamp());
			if (maxTakenDate != null) dictionary.Add("max_taken_date", maxTakenDate.Value.ToUnixTimestamp());
			return GetResponse<PlaceCollection>(dictionary);
		}

		public PlaceCollection PlacesPlacesForContacts() 
		{
			return PlacesPlacesForContacts(PlaceType.None, null, null, null, null, null, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForContacts(string woeId) 
		{
			return PlacesPlacesForContacts(PlaceType.None, null, null, woeId, null, null, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForContacts(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesPlacesForContacts(PlaceType.None, null, null, woeId, null, null, minUploadDate, maxUploadDate, null, null);
		}


		public PlaceCollection PlacesPlacesForContacts(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesPlacesForContacts(PlaceType.None, null, null, woeId, null, null, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}

		#endregion

		#region flickr.places.placesForTags

		public PlaceCollection PlacesPlacesForTags(PlaceType placeTypeId, string woeId, string placeId, int? threshold, IEnumerable<string> tags, TagMode tagMode, IEnumerable<string> machineTags, MachineTagMode machineTagMode, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.placesForTags");
			if (placeTypeId != PlaceType.None) dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (woeId != null) dictionary.Add("woe_id", woeId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (threshold != null) dictionary.Add("threshold", threshold.ToString().ToLower());
			if (tags != null) dictionary.Add("tags", tags == null ? String.Empty : String.Join(",", tags.ToArray()));
			if (tagMode != TagMode.None) dictionary.Add("tag_mode", tagMode.ToString().ToLower());
			if (machineTags != null) dictionary.Add("machine_tags", machineTags == null ? String.Empty : String.Join(",", machineTags.ToArray()));
			if (machineTagMode != MachineTagMode.None) dictionary.Add("machine_tag_mode", machineTagMode.ToString().ToLower());
			if (minUploadDate != null) dictionary.Add("min_upload_date", minUploadDate.Value.ToUnixTimestamp());
			if (maxUploadDate != null) dictionary.Add("max_upload_date", maxUploadDate.Value.ToUnixTimestamp());
			if (minTakenDate != null) dictionary.Add("min_taken_date", minTakenDate.Value.ToUnixTimestamp());
			if (maxTakenDate != null) dictionary.Add("max_taken_date", maxTakenDate.Value.ToUnixTimestamp());
			return GetResponse<PlaceCollection>(dictionary);
		}

		public PlaceCollection PlacesPlacesForTags() 
		{
			return PlacesPlacesForTags(PlaceType.None, null, null, null, null, TagMode.None, null, MachineTagMode.None, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, null, TagMode.None, null, MachineTagMode.None, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, null, TagMode.None, null, MachineTagMode.None, minUploadDate, maxUploadDate, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, null, TagMode.None, null, MachineTagMode.None, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> tags) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, tags, TagMode.None, null, MachineTagMode.None, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> tags, TagMode tagMode) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, tags, tagMode, null, MachineTagMode.None, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> machineTags, MachineTagMode machineTagMode) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, null, TagMode.None, machineTags, machineTagMode, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> tags, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, tags, TagMode.None, null, MachineTagMode.None, minUploadDate, maxUploadDate, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> tags, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, tags, TagMode.None, null, MachineTagMode.None, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> tags, TagMode tagMode, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, tags, tagMode, null, MachineTagMode.None, minUploadDate, maxUploadDate, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> tags, TagMode tagMode, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, tags, tagMode, null, MachineTagMode.None, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> machineTags, MachineTagMode machineTagMode, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, null, TagMode.None, machineTags, machineTagMode, minUploadDate, maxUploadDate, null, null);
		}


		public PlaceCollection PlacesPlacesForTags(string woeId, IEnumerable<string> machineTags, MachineTagMode machineTagMode, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesPlacesForTags(PlaceType.None, woeId, null, null, null, TagMode.None, machineTags, machineTagMode, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}

		#endregion

		#region flickr.places.placesForUser

		public PlaceCollection PlacesPlacesForUser(PlaceType placeTypeId, string placeType, string woeId, string placeId, int? threshold, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.placesForUser");
			if (placeTypeId != PlaceType.None) dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (placeType != null) dictionary.Add("place_type", placeType);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (threshold != null) dictionary.Add("threshold", threshold.ToString().ToLower());
			if (minUploadDate != null) dictionary.Add("min_upload_date", minUploadDate.Value.ToUnixTimestamp());
			if (maxUploadDate != null) dictionary.Add("max_upload_date", maxUploadDate.Value.ToUnixTimestamp());
			if (minTakenDate != null) dictionary.Add("min_taken_date", minTakenDate.Value.ToUnixTimestamp());
			if (maxTakenDate != null) dictionary.Add("max_taken_date", maxTakenDate.Value.ToUnixTimestamp());
			return GetResponse<PlaceCollection>(dictionary);
		}

		public PlaceCollection PlacesPlacesForUser() 
		{
			return PlacesPlacesForUser(PlaceType.None, null, null, null, null, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForUser(string woeId) 
		{
			return PlacesPlacesForUser(PlaceType.None, null, woeId, null, null, null, null, null, null);
		}


		public PlaceCollection PlacesPlacesForUser(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesPlacesForUser(PlaceType.None, null, woeId, null, null, minUploadDate, maxUploadDate, null, null);
		}


		public PlaceCollection PlacesPlacesForUser(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesPlacesForUser(PlaceType.None, null, woeId, null, null, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}

		#endregion

		#region flickr.places.tagsForPlace

		public TagCollection PlacesTagsForPlace(string woeId, string placeId, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.tagsForPlace");
			if (woeId != null) dictionary.Add("woe_id", woeId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (minUploadDate != null) dictionary.Add("min_upload_date", minUploadDate.Value.ToUnixTimestamp());
			if (maxUploadDate != null) dictionary.Add("max_upload_date", maxUploadDate.Value.ToUnixTimestamp());
			if (minTakenDate != null) dictionary.Add("min_taken_date", minTakenDate.Value.ToUnixTimestamp());
			if (maxTakenDate != null) dictionary.Add("max_taken_date", maxTakenDate.Value.ToUnixTimestamp());
			return GetResponse<TagCollection>(dictionary);
		}

		public TagCollection PlacesTagsForPlace(string woeId) 
		{
			return PlacesTagsForPlace(woeId, null, null, null, null, null);
		}


		public TagCollection PlacesTagsForPlace(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate) 
		{
			return PlacesTagsForPlace(woeId, null, minUploadDate, maxUploadDate, null, null);
		}


		public TagCollection PlacesTagsForPlace(string woeId, DateTime? minUploadDate, DateTime? maxUploadDate, DateTime? minTakenDate, DateTime? maxTakenDate) 
		{
			return PlacesTagsForPlace(woeId, null, minUploadDate, maxUploadDate, minTakenDate, maxTakenDate);
		}


		public TagCollection PlacesTagsForPlace(string woeId, string placeId) 
		{
			return PlacesTagsForPlace(woeId, placeId, null, null, null, null);
		}

		#endregion

		#region flickr.prefs.getContentType

		public ContentType PrefsGetContentType() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getContentType");
			var result = GetResponse<UnknownResponse>(dictionary);
			return result.GetAttributeValue<ContentType>("person", "content_type");
		}
		#endregion

		#region flickr.prefs.getGeoPerms

		public UserGeoPermissions PrefsGetGeoPerms() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getGeoPerms");
			return GetResponse<UserGeoPermissions>(dictionary);
		}
		#endregion

		#region flickr.prefs.getHidden

		public HiddenFromSearch PrefsGetHidden() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getHidden");
			var result = GetResponse<UnknownResponse>(dictionary);
			return result.GetAttributeValue<HiddenFromSearch>("person", "hidden");
		}
		#endregion

		#region flickr.prefs.getPrivacy

		public PrivacyFilter PrefsGetPrivacy() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getPrivacy");
			var result = GetResponse<UnknownResponse>(dictionary);
			return result.GetAttributeValue<PrivacyFilter>("person", "privacy");
		}
		#endregion

		#region flickr.prefs.getSafetyLevel

		public SafetyLevel PrefsGetSafetyLevel() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getSafetyLevel");
			var result = GetResponse<UnknownResponse>(dictionary);
			return result.GetAttributeValue<SafetyLevel>("person", "safety_level");
		}
		#endregion

		#region flickr.push.getSubscriptions

		public SubscriptionCollection PushGetSubscriptions() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.getSubscriptions");
			return GetResponse<SubscriptionCollection>(dictionary);
		}
		#endregion

		#region flickr.push.getTopics

		public string[] PushGetTopics() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.getTopics");
			var result = GetResponse<UnknownResponse>(dictionary);
			return result.GetElementArray<string>("topic", "name");
		}
		#endregion

		#region flickr.push.subscribe

		public void PushSubscribe(string topic, string callback, string verify, string verifyToken, int? leaseSeconds, IEnumerable<int> woeIds, IEnumerable<string> placeIds, double? lat, double? lon, int? radius, RadiusUnit radiusUnits, GeoAccuracy accuracy, IEnumerable<string> nsids, IEnumerable<string> tags) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.subscribe");
			dictionary.Add("topic", topic);
			dictionary.Add("callback", callback);
			dictionary.Add("verify", verify);
			if (verifyToken != null) dictionary.Add("verify_token", verifyToken);
			if (leaseSeconds != null) dictionary.Add("lease_seconds", leaseSeconds.ToString().ToLower());
			if (woeIds != null) dictionary.Add("woe_ids", woeIds == null ? String.Empty : String.Join(",", woeIds.Select(d => d.ToString(CultureInfo.InvariantCulture)).ToArray()));
			if (placeIds != null) dictionary.Add("place_ids", placeIds == null ? String.Empty : String.Join(",", placeIds.ToArray()));
			if (lat != null) dictionary.Add("lat", lat.ToString().ToLower());
			if (lon != null) dictionary.Add("lon", lon.ToString().ToLower());
			if (radius != null) dictionary.Add("radius", radius.ToString().ToLower());
			if (radiusUnits != RadiusUnit.None) dictionary.Add("radius_units", radiusUnits.ToString().ToLower());
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (nsids != null) dictionary.Add("nsids", nsids == null ? String.Empty : String.Join(",", nsids.ToArray()));
			if (tags != null) dictionary.Add("tags", tags == null ? String.Empty : String.Join(",", tags.ToArray()));
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.push.unsubscribe

		public void PushUnsubscribe(string topic, string callback, string verify, string verifyToken) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.unsubscribe");
			dictionary.Add("topic", topic);
			dictionary.Add("callback", callback);
			dictionary.Add("verify", verify);
			if (verifyToken != null) dictionary.Add("verify_token", verifyToken);
			GetResponse<NoResponse>(dictionary);
		}

		public void PushUnsubscribe(string topic, string callback, string verify) 
		{
			PushUnsubscribe(topic, callback, verify, null);
		}

		#endregion

		#region flickr.reflection.getMethodInfo

		public Method ReflectionGetMethodInfo(string methodName) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.reflection.getMethodInfo");
			dictionary.Add("method_name", methodName);
			return GetResponse<Method>(dictionary);
		}
		#endregion

		#region flickr.reflection.getMethods

		public MethodCollection ReflectionGetMethods() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.reflection.getMethods");
			return GetResponse<MethodCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getCollectionDomains

		public StatDomainCollection StatsGetCollectionDomains(DateTime date, string collectionId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCollectionDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (collectionId != null) dictionary.Add("collection_id", collectionId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatDomainCollection>(dictionary);
		}

		public StatDomainCollection StatsGetCollectionDomains(DateTime date) 
		{
			return StatsGetCollectionDomains(date, null, 0, 0);
		}


		public StatDomainCollection StatsGetCollectionDomains(DateTime date, string collectionId) 
		{
			return StatsGetCollectionDomains(date, collectionId, 0, 0);
		}


		public StatDomainCollection StatsGetCollectionDomains(DateTime date, int page, int perPage) 
		{
			return StatsGetCollectionDomains(date, null, page, perPage);
		}

		#endregion

		#region flickr.stats.getCollectionReferrers

		public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain, string collectionId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCollectionReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (collectionId != null) dictionary.Add("collection_id", collectionId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatReferrerCollection>(dictionary);
		}

		public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain) 
		{
			return StatsGetCollectionReferrers(date, domain, null, 0, 0);
		}


		public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain, string collectionId) 
		{
			return StatsGetCollectionReferrers(date, domain, collectionId, 0, 0);
		}


		public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain, int page, int perPage) 
		{
			return StatsGetCollectionReferrers(date, domain, null, page, perPage);
		}

		#endregion

		#region flickr.stats.getCollectionStats

		public Stats StatsGetCollectionStats(DateTime date, string collectionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCollectionStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("collection_id", collectionId);
			return GetResponse<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getCSVFiles

		public CsvFileCollection StatsGetCSVFiles() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCSVFiles");
			return GetResponse<CsvFileCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotoDomains

		public StatDomainCollection StatsGetPhotoDomains(DateTime date, string photoId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotoDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (photoId != null) dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatDomainCollection>(dictionary);
		}

		public StatDomainCollection StatsGetPhotoDomains(DateTime date) 
		{
			return StatsGetPhotoDomains(date, null, 0, 0);
		}


		public StatDomainCollection StatsGetPhotoDomains(DateTime date, string photoId) 
		{
			return StatsGetPhotoDomains(date, photoId, 0, 0);
		}


		public StatDomainCollection StatsGetPhotoDomains(DateTime date, int page, int perPage) 
		{
			return StatsGetPhotoDomains(date, null, page, perPage);
		}

		#endregion

		#region flickr.stats.getPhotoReferrers

		public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain, string photoId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotoReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (photoId != null) dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatReferrerCollection>(dictionary);
		}

		public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain) 
		{
			return StatsGetPhotoReferrers(date, domain, null, 0, 0);
		}


		public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain, string photoId) 
		{
			return StatsGetPhotoReferrers(date, domain, photoId, 0, 0);
		}


		public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain, int page, int perPage) 
		{
			return StatsGetPhotoReferrers(date, domain, null, page, perPage);
		}

		#endregion

		#region flickr.stats.getPhotosetDomains

		public StatDomainCollection StatsGetPhotosetDomains(DateTime date, string photosetId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotosetDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (photosetId != null) dictionary.Add("photoset_id", photosetId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatDomainCollection>(dictionary);
		}

		public StatDomainCollection StatsGetPhotosetDomains(DateTime date) 
		{
			return StatsGetPhotosetDomains(date, null, 0, 0);
		}


		public StatDomainCollection StatsGetPhotosetDomains(DateTime date, string photosetId) 
		{
			return StatsGetPhotosetDomains(date, photosetId, 0, 0);
		}


		public StatDomainCollection StatsGetPhotosetDomains(DateTime date, int page, int perPage) 
		{
			return StatsGetPhotosetDomains(date, null, page, perPage);
		}

		#endregion

		#region flickr.stats.getPhotosetReferrers

		public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain, string photosetId, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotosetReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (photosetId != null) dictionary.Add("photoset_id", photosetId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatReferrerCollection>(dictionary);
		}

		public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain) 
		{
			return StatsGetPhotosetReferrers(date, domain, null, 0, 0);
		}


		public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain, string photosetId) 
		{
			return StatsGetPhotosetReferrers(date, domain, photosetId, 0, 0);
		}


		public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain, int page, int perPage) 
		{
			return StatsGetPhotosetReferrers(date, domain, null, page, perPage);
		}

		#endregion

		#region flickr.stats.getPhotosetStats

		public Stats StatsGetPhotosetStats(DateTime date, string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotosetStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("photoset_id", photosetId);
			return GetResponse<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotoStats

		public Stats StatsGetPhotoStats(DateTime date, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotoStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("photo_id", photoId);
			return GetResponse<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotostreamDomains

		public StatDomainCollection StatsGetPhotostreamDomains(DateTime date, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotostreamDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatDomainCollection>(dictionary);
		}

		public StatDomainCollection StatsGetPhotostreamDomains(DateTime date) 
		{
			return StatsGetPhotostreamDomains(date, 0, 0);
		}

		#endregion

		#region flickr.stats.getPhotostreamReferrers

		public StatReferrerCollection StatsGetPhotostreamReferrers(DateTime date, string domain, int perPage, int page) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotostreamReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			return GetResponse<StatReferrerCollection>(dictionary);
		}

		public StatReferrerCollection StatsGetPhotostreamReferrers(DateTime date, string domain) 
		{
			return StatsGetPhotostreamReferrers(date, domain, 0, 0);
		}

		#endregion

		#region flickr.stats.getPhotostreamStats

		public Stats StatsGetPhotostreamStats(DateTime date) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotostreamStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			return GetResponse<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getPopularPhotos

		public PopularPhotoCollection StatsGetPopularPhotos(DateTime? date, PopularitySort sort, int page, int perPage) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPopularPhotos");
			if (date != null) dictionary.Add("date", date.Value.ToUnixTimestamp());
			if (sort != PopularitySort.None) dictionary.Add("sort", sort.ToString().ToLower());
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return GetResponse<PopularPhotoCollection>(dictionary);
		}

		public PopularPhotoCollection StatsGetPopularPhotos() 
		{
			return StatsGetPopularPhotos(null, PopularitySort.None, 0, 0);
		}


		public PopularPhotoCollection StatsGetPopularPhotos(DateTime? date) 
		{
			return StatsGetPopularPhotos(date, PopularitySort.None, 0, 0);
		}


		public PopularPhotoCollection StatsGetPopularPhotos(DateTime? date, int page, int perPage) 
		{
			return StatsGetPopularPhotos(date, PopularitySort.None, page, perPage);
		}


		public PopularPhotoCollection StatsGetPopularPhotos(int page, int perPage) 
		{
			return StatsGetPopularPhotos(null, PopularitySort.None, page, perPage);
		}


		public PopularPhotoCollection StatsGetPopularPhotos(PopularitySort sort) 
		{
			return StatsGetPopularPhotos(null, sort, 0, 0);
		}


		public PopularPhotoCollection StatsGetPopularPhotos(PopularitySort sort, int page, int perPage) 
		{
			return StatsGetPopularPhotos(null, sort, page, perPage);
		}

		#endregion

		#region flickr.stats.getTotalViews

		public StatViews StatsGetTotalViews(DateTime? date) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getTotalViews");
			if (date != null) dictionary.Add("date", date.Value.ToUnixTimestamp());
			return GetResponse<StatViews>(dictionary);
		}

		public StatViews StatsGetTotalViews() 
		{
			return StatsGetTotalViews(null);
		}

		#endregion

		#region flickr.tags.getClusterPhotos

		public PhotoCollection TagsGetClusterPhotos(string tag, string clusterId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getClusterPhotos");
			dictionary.Add("tag", tag);
			dictionary.Add("cluster_id", clusterId);
			return GetResponse<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getClusters

		public ClusterCollection TagsGetClusters(string tag) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getClusters");
			dictionary.Add("tag", tag);
			return GetResponse<ClusterCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getHotList

		public HotTagCollection TagsGetHotList(string period, int? count) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getHotList");
			if (period != null) dictionary.Add("period", period);
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			return GetResponse<HotTagCollection>(dictionary);
		}

		public HotTagCollection TagsGetHotList() 
		{
			return TagsGetHotList(null, null);
		}


		public HotTagCollection TagsGetHotList(string period) 
		{
			return TagsGetHotList(period, null);
		}

		#endregion

		#region flickr.tags.getListPhoto

		public PhotoInfoTagCollection TagsGetListPhoto(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListPhoto");
			dictionary.Add("photo_id", photoId);
			return GetResponse<PhotoInfoTagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getListUser

		public TagCollection TagsGetListUser(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListUser");
			if (userId != null) dictionary.Add("user_id", userId);
			return GetResponse<TagCollection>(dictionary);
		}

		public TagCollection TagsGetListUser() 
		{
			return TagsGetListUser(null);
		}

		#endregion

		#region flickr.tags.getListUserPopular

		public TagCollection TagsGetListUserPopular(string userId, int? count) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListUserPopular");
			if (userId != null) dictionary.Add("user_id", userId);
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			return GetResponse<TagCollection>(dictionary);
		}

		public TagCollection TagsGetListUserPopular() 
		{
			return TagsGetListUserPopular(null, null);
		}


		public TagCollection TagsGetListUserPopular(string userId) 
		{
			return TagsGetListUserPopular(userId, null);
		}


		public TagCollection TagsGetListUserPopular(int? count) 
		{
			return TagsGetListUserPopular(null, count);
		}

		#endregion

		#region flickr.tags.getListUserRaw

		public RawTagCollection TagsGetListUserRaw(string tag) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListUserRaw");
			if (tag != null) dictionary.Add("tag", tag);
			return GetResponse<RawTagCollection>(dictionary);
		}

		public RawTagCollection TagsGetListUserRaw() 
		{
			return TagsGetListUserRaw(null);
		}

		#endregion

		#region flickr.tags.getMostFrequentlyUsed

		public TagCollection TagsGetMostFrequentlyUsed() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getMostFrequentlyUsed");
			return GetResponse<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getRelated

		public TagCollection TagsGetRelated(string tag) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getRelated");
			dictionary.Add("tag", tag);
			return GetResponse<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.test.echo

		public EchoResponseDictionary TestEcho(Dictionary<string,string> parameters) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.test.echo");
			dictionary.Add("parameters", parameters.ToString().ToLower());
			return GetResponse<EchoResponseDictionary>(dictionary);
		}
		#endregion

		#region flickr.test.login

		public FoundUser TestLogin() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.test.login");
			return GetResponse<FoundUser>(dictionary);
		}
		#endregion

		#region flickr.test.null

		public void TestNull() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.test.null");
			GetResponse<NoResponse>(dictionary);
		}
		#endregion

		#region flickr.urls.getGroup

		public string UrlsGetGroup(string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.getGroup");
			dictionary.Add("group_id", groupId);
			var result = GetResponse<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.getUserPhotos

		public string UrlsGetUserPhotos(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.getUserPhotos");
			if (userId != null) dictionary.Add("user_id", userId);
			var result = GetResponse<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.getUserProfile

		public string UrlsGetUserProfile(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.getUserProfile");
			if (userId != null) dictionary.Add("user_id", userId);
			var result = GetResponse<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.lookupGallery

		public Gallery UrlsLookupGallery(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.lookupGallery");
			dictionary.Add("url", url);
			return GetResponse<Gallery>(dictionary);
		}
		#endregion

		#region flickr.urls.lookupGroup

		public string UrlsLookupGroup(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.lookupGroup");
			dictionary.Add("url", url);
			var result = GetResponse<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.lookupUser

		public FoundUser UrlsLookupUser(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.lookupUser");
			dictionary.Add("url", url);
			return GetResponse<FoundUser>(dictionary);
		}
		#endregion
	}

}
