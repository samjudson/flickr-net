
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable UseObjectOrCollectionInitializer

namespace FlickrNet
{
	public partial class Flickr
	{

		#region flickr.auth.oauth.checkToken

		public async Task<OAuthAccessToken> AuthOauthCheckTokenAsync(string oauthToken) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.auth.oauth.checkToken");
			dictionary.Add("oauth_token", oauthToken);
			return await GetResponseAsync<OAuthAccessToken>(dictionary);
		}
		#endregion

		#region flickr.activity.userComments

		public async Task<ActivityItemCollection> ActivityUserCommentsAsync(int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.activity.userComments");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<ActivityItemCollection>(dictionary);
		}
		#endregion

		#region flickr.activity.userPhotos

		public async Task<ActivityItemCollection> ActivityUserPhotosAsync(string timeframe = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.activity.userPhotos");
			if (timeframe != null) dictionary.Add("timeframe", timeframe);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<ActivityItemCollection>(dictionary);
		}
		#endregion

		#region flickr.blogs.getList

		public async Task<BlogCollection> BlogsGetListAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.blogs.getList");
			return await GetResponseAsync<BlogCollection>(dictionary);
		}
		#endregion

		#region flickr.blogs.getServices

		public async Task<BlogServiceCollection> BlogsGetServicesAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.blogs.getServices");
			return await GetResponseAsync<BlogServiceCollection>(dictionary);
		}
		#endregion

		#region flickr.blogs.postPhoto

		public async Task BlogsPostPhotoAsync(string photoId, string title, string description, int blogId = 0, string blogPassword = null, string service = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.blogs.postPhoto");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			if (blogId != 0) dictionary.Add("blog_id", blogId.ToString(CultureInfo.InvariantCulture));
			if (blogPassword != null) dictionary.Add("blog_password", blogPassword);
			if (service != null) dictionary.Add("service", service);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.cameras.getBrandModels

		public async Task<CameraCollection> CamerasGetBrandModelsAsync(string brand) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.cameras.getBrandModels");
			dictionary.Add("brand", brand);
			return await GetResponseAsync<CameraCollection>(dictionary);
		}
		#endregion

		#region flickr.cameras.getBrands

		public async Task<BrandCollection> CamerasGetBrandsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.cameras.getBrands");
			return await GetResponseAsync<BrandCollection>(dictionary);
		}
		#endregion

		#region flickr.collections.getInfo

		public async Task<CollectionInfo> CollectionsGetInfoAsync(string collectionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.collections.getInfo");
			dictionary.Add("collection_id", collectionId);
			return await GetResponseAsync<CollectionInfo>(dictionary);
		}
		#endregion

		#region flickr.collections.getTree

		public async Task<CollectionCollection> CollectionsGetTreeAsync(string collectionId = null, string userId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.collections.getTree");
			if (collectionId != null) dictionary.Add("collection_id", collectionId);
			if (userId != null) dictionary.Add("user_id", userId);
			return await GetResponseAsync<CollectionCollection>(dictionary);
		}
		#endregion

		#region flickr.commons.getInstitutions

		public async Task<InstitutionCollection> CommonsGetInstitutionsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.commons.getInstitutions");
			return await GetResponseAsync<InstitutionCollection>(dictionary);
		}
		#endregion

		#region flickr.contacts.getList

		public async Task<ContactCollection> ContactsGetListAsync(string filter = null, int page = 0, int perPage = 0, string sort = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getList");
			if (filter != null) dictionary.Add("filter", filter);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (sort != null) dictionary.Add("sort", sort);
			return await GetResponseAsync<ContactCollection>(dictionary);
		}
		#endregion

		#region flickr.contacts.getListRecentlyUploaded

		public async Task<ContactCollection> ContactsGetListRecentlyUploadedAsync(DateTime? dateLastupdated = null, string filter = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getListRecentlyUploaded");
			if (dateLastupdated != null) dictionary.Add("date_lastupdated", dateLastupdated.Value.ToUnixTimestamp());
			if (filter != null) dictionary.Add("filter", filter);
			return await GetResponseAsync<ContactCollection>(dictionary);
		}
		#endregion

		#region flickr.contacts.getPublicList

		public async Task<ContactCollection> ContactsGetPublicListAsync(string userId, int perPage = 0, int page = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getPublicList");
			dictionary.Add("user_id", userId);
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<ContactCollection>(dictionary);
		}
		#endregion

		#region flickr.contacts.getTaggingSuggestions

		public async Task<ContactCollection> ContactsGetTaggingSuggestionsAsync(int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.contacts.getTaggingSuggestions");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<ContactCollection>(dictionary);
		}
		#endregion

		#region flickr.favorites.add

		public async Task FavoritesAddAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.add");
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.favorites.getContext

		public async Task<FavoriteContext> FavoritesGetContextAsync(string photoId, string userId, int numPrev = 1, int numNext = 1, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.getContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			if (numPrev != 1) dictionary.Add("num_prev", numPrev.ToString(CultureInfo.InvariantCulture));
			if (numNext != 1) dictionary.Add("num_next", numNext.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<FavoriteContext>(dictionary);
		}
		#endregion

		#region flickr.favorites.getList

		public async Task<PhotoCollection> FavoritesGetListAsync(string userId = null, DateTime? minFaveDate = null, DateTime? maxFaveDate = null, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.getList");
			if (userId != null) dictionary.Add("user_id", userId);
			if (minFaveDate != null) dictionary.Add("min_fave_date", minFaveDate.Value.ToUnixTimestamp());
			if (maxFaveDate != null) dictionary.Add("max_fave_date", maxFaveDate.Value.ToUnixTimestamp());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.favorites.getPublicList

		public async Task<PhotoCollection> FavoritesGetPublicListAsync(string userId, DateTime? minFaveDate = null, DateTime? maxFaveDate = null, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.getPublicList");
			dictionary.Add("user_id", userId);
			if (minFaveDate != null) dictionary.Add("min_fave_date", minFaveDate.Value.ToUnixTimestamp());
			if (maxFaveDate != null) dictionary.Add("max_fave_date", maxFaveDate.Value.ToUnixTimestamp());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.favorites.remove

		public async Task FavoritesRemoveAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.favorites.remove");
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.galleries.addPhoto

		public async Task GalleriesAddPhotoAsync(string galleryId, string photoId, string comment = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.addPhoto");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("photo_id", photoId);
			if (comment != null) dictionary.Add("comment", comment);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.galleries.create

		public async Task GalleriesCreateAsync(string title, string description, string primaryPhotoId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.create");
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			if (primaryPhotoId != null) dictionary.Add("primary_photo_id", primaryPhotoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.galleries.editMeta

		public async Task GalleriesEditMetaAsync(string galleryId, string title, string description = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.editMeta");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("title", title);
			if (description != null) dictionary.Add("description", description);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.galleries.editPhoto

		public async Task GalleriesEditPhotoAsync(string galleryId, string photoId, string comment) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.editPhoto");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("photo_id", photoId);
			dictionary.Add("comment", comment);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.galleries.editPhotos

		public async Task GalleriesEditPhotosAsync(string galleryId, string primaryPhotoId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.editPhotos");
			dictionary.Add("gallery_id", galleryId);
			dictionary.Add("primary_photo_id", primaryPhotoId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.galleries.getInfo

		public async Task<Gallery> GalleriesGetInfoAsync(string galleryId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getInfo");
			dictionary.Add("gallery_id", galleryId);
			return await GetResponseAsync<Gallery>(dictionary);
		}
		#endregion

		#region flickr.galleries.getList

		public async Task<GalleryCollection> GalleriesGetListAsync(string userId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getList");
			if (userId != null) dictionary.Add("user_id", userId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<GalleryCollection>(dictionary);
		}
		#endregion

		#region flickr.galleries.getListForPhoto

		public async Task<GalleryCollection> GalleriesGetListForPhotoAsync(string photoId, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getListForPhoto");
			dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<GalleryCollection>(dictionary);
		}
		#endregion

		#region flickr.galleries.getPhotos

		public async Task<GalleryPhotoCollection> GalleriesGetPhotosAsync(string galleryId, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.galleries.getPhotos");
			dictionary.Add("gallery_id", galleryId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<GalleryPhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.groups.browse

		public async Task<GroupCategory> GroupsBrowseAsync(string catId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.browse");
			if (catId != null) dictionary.Add("cat_id", catId);
			return await GetResponseAsync<GroupCategory>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.add

		public async Task GroupsDiscussRepliesAddAsync(string topicId, string message) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.add");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("message", message);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.delete

		public async Task GroupsDiscussRepliesDeleteAsync(string topicId, string replyId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.delete");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("reply_id", replyId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.edit

		public async Task GroupsDiscussRepliesEditAsync(string topicId, string replyId, string message) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.edit");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("reply_id", replyId);
			dictionary.Add("message", message);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.getInfo

		public async Task<TopicReply> GroupsDiscussRepliesGetInfoAsync(string topicId, string replyId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.getInfo");
			dictionary.Add("topic_id", topicId);
			dictionary.Add("reply_id", replyId);
			return await GetResponseAsync<TopicReply>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.replies.getList

		public async Task<TopicReplyCollection> GroupsDiscussRepliesGetListAsync(string topicId, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.replies.getList");
			dictionary.Add("topic_id", topicId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<TopicReplyCollection>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.topics.add

		public async Task GroupsDiscussTopicsAddAsync(string groupId, string subject, string message) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.topics.add");
			dictionary.Add("group_id", groupId);
			dictionary.Add("subject", subject);
			dictionary.Add("message", message);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.topics.getInfo
		/// <summary>Gets information on a particular topic with a group.</summary>
		/// <param name="topicId">The id of the topic you with information on.</param>
		/// <returns></returns>
		public async Task<Topic> GroupsDiscussTopicsGetInfoAsync(string topicId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.topics.getInfo");
			dictionary.Add("topic_id", topicId);
			return await GetResponseAsync<Topic>(dictionary);
		}
		#endregion

		#region flickr.groups.discuss.topics.getList
		/// <summary>
		/// Gets a list of topics for a particular group.
		/// </summary>
		/// <param name="groupId">The id of the group.</param>
		/// <param name="page">The page of topics you wish to return.</param>
		/// <param name="perPage">The number of topics per page to return.</param>
		/// <returns></returns>
		public async Task<TopicCollection> GroupsDiscussTopicsGetListAsync(string groupId, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.discuss.topics.getList");
			dictionary.Add("group_id", groupId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<TopicCollection>(dictionary);
		}
		#endregion

		#region flickr.groups.getInfo

		public async Task<GroupFullInfo> GroupsGetInfoAsync(string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.getInfo");
			dictionary.Add("group_id", groupId);
			return await GetResponseAsync<GroupFullInfo>(dictionary);
		}
		#endregion

		#region flickr.groups.join

		public async Task GroupsJoinAsync(string groupId, bool acceptRules) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.join");
			dictionary.Add("group_id", groupId);
			dictionary.Add("accept_rules", acceptRules ? "1" : "0");
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.join.request

		public async Task GroupsJoinRequestAsync(string groupId, string message, bool acceptRules) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.join.request");
			dictionary.Add("group_id", groupId);
			dictionary.Add("message", message);
			dictionary.Add("accept_rules", acceptRules ? "1" : "0");
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.leave

		public async Task GroupsLeaveAsync(string groupId, bool? deletePhotos = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.leave");
			dictionary.Add("group_id", groupId);
			if (deletePhotos != null) dictionary.Add("delete_photos", deletePhotos.Value ? "1" : "0");
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.members.getList

		public async Task<MemberCollection> GroupsMembersGetListAsync(string groupId, int page = 0, int perPage = 0, MemberTypes memberType = MemberTypes.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.members.getList");
			dictionary.Add("group_id", groupId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (memberType != MemberTypes.None) dictionary.Add("member_type", memberType.ToString().ToLower());
			return await GetResponseAsync<MemberCollection>(dictionary);
		}
		#endregion

		#region flickr.groups.pools.add

		public async Task GroupsPoolsAddAsync(string photoId, string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.add");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("group_id", groupId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.pools.getContext

		public async Task<Context> GroupsPoolsGetContextAsync(string photoId, string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.getContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("group_id", groupId);
			return await GetResponseAsync<Context>(dictionary);
		}
		#endregion

		#region flickr.groups.pools.getGroups

		public async Task<MemberGroupInfoCollection> GroupsPoolsGetGroupsAsync(int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.getGroups");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<MemberGroupInfoCollection>(dictionary);
		}
		#endregion

		#region flickr.groups.pools.getPhotos

		public async Task<PhotoCollection> GroupsPoolsGetPhotosAsync(string groupId, string tags = null, string userId = null, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.getPhotos");
			dictionary.Add("group_id", groupId);
			if (tags != null) dictionary.Add("tags", tags);
			if (userId != null) dictionary.Add("user_id", userId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.groups.pools.remove

		public async Task GroupsPoolsRemoveAsync(string photoId, string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.pools.remove");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("group_id", groupId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.groups.search

		public async Task<GroupSearchResultCollection> GroupsSearchAsync(string text, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.groups.search");
			dictionary.Add("text", text);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<GroupSearchResultCollection>(dictionary);
		}
		#endregion

		#region flickr.interestingness.getList

		public async Task<PhotoCollection> InterestingnessGetListAsync(DateTime? date = null, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.interestingness.getList");
			if (date != null) dictionary.Add("date", date.Value.ToMySql());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.machinetags.getNamespaces

		public async Task<NamespaceCollection> MachinetagsGetNamespacesAsync(string predicate = null, int page = 0, int perPage = 1) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getNamespaces");
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<NamespaceCollection>(dictionary);
		}
		#endregion

		#region flickr.machinetags.getPairs

		public async Task<PairCollection> MachinetagsGetPairsAsync(string namespaceName = null, string predicate = null, int page = 0, int perPage = 1) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getPairs");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PairCollection>(dictionary);
		}
		#endregion

		#region flickr.machinetags.getPredicates

		public async Task<PredicateCollection> MachinetagsGetPredicatesAsync(string namespaceName = null, int page = 0, int perPage = 1) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getPredicates");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PredicateCollection>(dictionary);
		}
		#endregion

		#region flickr.machinetags.getRecentValues

		public async Task<ValueCollection> MachinetagsGetRecentValuesAsync(string namespaceName = null, string predicate = null, DateTime? addedSince = null, int page = 0, int perPage = 1) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getRecentValues");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (addedSince != null) dictionary.Add("added_since", addedSince.Value.ToUnixTimestamp());
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<ValueCollection>(dictionary);
		}
		#endregion

		#region flickr.machinetags.getValues

		public async Task<ValueCollection> MachinetagsGetValuesAsync(string namespaceName = null, string predicate = null, int page = 0, int perPage = 1) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.machinetags.getValues");
			if (namespaceName != null) dictionary.Add("namespace", namespaceName);
			if (predicate != null) dictionary.Add("predicate", predicate);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 1) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<ValueCollection>(dictionary);
		}
		#endregion

		#region flickr.panda.getList

		public async Task<PandaCollection> PandaGetListAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.panda.getList");
			return await GetResponseAsync<PandaCollection>(dictionary);
		}
		#endregion

		#region flickr.panda.getPhotos

		public async Task<PandaPhotoCollection> PandaGetPhotosAsync(string pandaName, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.panda.getPhotos");
			dictionary.Add("panda_name", pandaName);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PandaPhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.findByEmail

		public async Task<FoundUser> PeopleFindByEmailAsync(string findEmail) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.findByEmail");
			dictionary.Add("find_email", findEmail);
			return await GetResponseAsync<FoundUser>(dictionary);
		}
		#endregion

		#region flickr.people.findByUserName

		public async Task<FoundUser> PeopleFindByUserNameAsync(string username) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.findByUserName");
			dictionary.Add("username", username);
			return await GetResponseAsync<FoundUser>(dictionary);
		}
		#endregion

		#region flickr.people.getGroups

		public async Task<GroupInfoCollection> PeopleGetGroupsAsync(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getGroups");
			dictionary.Add("user_id", userId);
			return await GetResponseAsync<GroupInfoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.getInfo

		public async Task<Person> PeopleGetInfoAsync(string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getInfo");
			dictionary.Add("user_id", userId);
			return await GetResponseAsync<Person>(dictionary);
		}
		#endregion

		#region flickr.people.getLimits

		public async Task<PersonLimits> PeopleGetLimitsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getLimits");
			return await GetResponseAsync<PersonLimits>(dictionary);
		}
		#endregion

		#region flickr.people.getPhotos

		public async Task<PhotoCollection> PeopleGetPhotosAsync(string userId = "me", SafetyLevel safeSearch = SafetyLevel.None, DateTime? minUploadDate = null, DateTime? maxUploadDate = null, DateTime? minTakenDate = null, DateTime? maxTakenDate = null, ContentTypeSearch contentType = ContentTypeSearch.None, PrivacyFilter privacyFilter = PrivacyFilter.None, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPhotos");
			if (userId != "me") dictionary.Add("user_id", userId);
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
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.getPhotosOf

		public async Task<PeoplePhotoCollection> PeopleGetPhotosOfAsync(string userId = "me", PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPhotosOf");
			if (userId != "me") dictionary.Add("user_id", userId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PeoplePhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.getPublicGroups

		public async Task<GroupInfoCollection> PeopleGetPublicGroupsAsync(string userId, bool? includeInvitationOnly = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPublicGroups");
			dictionary.Add("user_id", userId);
			if (includeInvitationOnly != null) dictionary.Add("include_invitation_only", includeInvitationOnly.Value ? "1" : "0");
			return await GetResponseAsync<GroupInfoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.getPublicPhotos

		public async Task<PhotoCollection> PeopleGetPublicPhotosAsync(string userId, int page = 0, int perPage = 0, SafetyLevel safetyLevel = SafetyLevel.None, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getPublicPhotos");
			dictionary.Add("user_id", userId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (safetyLevel != SafetyLevel.None) dictionary.Add("safety_level", safetyLevel.ToString("d"));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.people.getUploadStatus

		public async Task<UserStatus> PeopleGetUploadStatusAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.people.getUploadStatus");
			return await GetResponseAsync<UserStatus>(dictionary);
		}
		#endregion

		#region flickr.photos.addTags

		public async Task PhotosAddTagsAsync(string photoId, IEnumerable<string> tags) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.addTags");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("tags", tags == null ? String.Empty : String.Join(",", tags.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.comments.addComment

		public async Task<string> PhotosCommentsAddCommentAsync(string photoId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.addComment");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("comment_text", commentText);
			var result = await GetResponseAsync<UnknownResponse>(dictionary);
			return result.GetAttributeValue<string>("comment", "id");
		}
		#endregion

		#region flickr.photos.comments.deleteComment

		public async Task PhotosCommentsDeleteCommentAsync(string commentId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.deleteComment");
			dictionary.Add("comment_id", commentId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.comments.editComment

		public async Task PhotosCommentsEditCommentAsync(string commentId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.editComment");
			dictionary.Add("comment_id", commentId);
			dictionary.Add("comment_text", commentText);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.comments.getList

		public async Task<PhotoCommentCollection> PhotosCommentsGetListAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.getList");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<PhotoCommentCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.comments.getRecentForContacts

		public async Task<PhotoCollection> PhotosCommentsGetRecentForContactsAsync(DateTime? dateLastComment = null, IEnumerable<string> contactsFilter = null, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.comments.getRecentForContacts");
			if (dateLastComment != null) dictionary.Add("date_last_comment", dateLastComment.Value.ToUnixTimestamp());
			if (contactsFilter != null) dictionary.Add("contacts_filter", contactsFilter == null ? String.Empty : String.Join(",", contactsFilter.ToArray()));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.delete

		public async Task PhotosDeleteAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.delete");
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.addPhoto

		public async Task PhotosetsAddPhotoAsync(string photosetId, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.addPhoto");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.comments.addComment

		public async Task<string> PhotosetsCommentsAddCommentAsync(string photosetId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.addComment");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("comment_text", commentText);
			var result = await GetResponseAsync<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.photosets.comments.deleteComment

		public async Task PhotosetsCommentsDeleteCommentAsync(string commentId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.deleteComment");
			dictionary.Add("comment_id", commentId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.comments.editComment

		public async Task PhotosetsCommentsEditCommentAsync(string commentId, string commentText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.editComment");
			dictionary.Add("comment_id", commentId);
			dictionary.Add("comment_text", commentText);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.comments.getList

		public async Task<PhotosetCommentCollection> PhotosetsCommentsGetListAsync(string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.comments.getList");
			dictionary.Add("photoset_id", photosetId);
			return await GetResponseAsync<PhotosetCommentCollection>(dictionary);
		}
		#endregion

		#region flickr.photosets.create

		public async Task<Photoset> PhotosetsCreateAsync(string title, string description, string primaryPhotoId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.create");
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			if (primaryPhotoId != null) dictionary.Add("primary_photo_id", primaryPhotoId);
			return await GetResponseAsync<Photoset>(dictionary);
		}
		#endregion

		#region flickr.photosets.delete

		public async Task PhotosetsDeleteAsync(string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.delete");
			dictionary.Add("photoset_id", photosetId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.editMeta

		public async Task PhotosetsEditMetaAsync(string photosetId, string title, string description) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.editMeta");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.editPhotos

		public async Task PhotosetsEditPhotosAsync(string photosetId, string primaryPhotoId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.editPhotos");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("primary_photo_id", primaryPhotoId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.getContext

		public async Task<Context> PhotosetsGetContextAsync(string photoId, string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("photoset_id", photosetId);
			return await GetResponseAsync<Context>(dictionary);
		}
		#endregion

		#region flickr.photosets.getInfo

		public async Task<Photoset> PhotosetsGetInfoAsync(string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getInfo");
			dictionary.Add("photoset_id", photosetId);
			return await GetResponseAsync<Photoset>(dictionary);
		}
		#endregion

		#region flickr.photosets.getList

		public async Task<PhotosetCollection> PhotosetsGetListAsync(string userId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getList");
			if (userId != null) dictionary.Add("user_id", userId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotosetCollection>(dictionary);
		}
		#endregion

		#region flickr.photosets.getPhotos

		public async Task<PhotosetPhotoCollection> PhotosetsGetPhotosAsync(string photosetId, PhotoSearchExtras extras = PhotoSearchExtras.None, PrivacyFilter privacyFilter = PrivacyFilter.None, int page = 0, int perPage = 0, MediaType media = MediaType.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.getPhotos");
			dictionary.Add("photoset_id", photosetId);
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (privacyFilter != PrivacyFilter.None) dictionary.Add("privacy_filter", privacyFilter.ToString("d"));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (media != MediaType.None) dictionary.Add("media", media.ToString().ToLower());
			return await GetResponseAsync<PhotosetPhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photosets.orderSets

		public async Task PhotosetsOrderSetsAsync(IEnumerable<string> photosetIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.orderSets");
			dictionary.Add("photoset_ids", photosetIds == null ? String.Empty : String.Join(",", photosetIds.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.removePhoto

		public async Task PhotosetsRemovePhotoAsync(string photosetId, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.removePhoto");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.removePhotos

		public async Task PhotosetsRemovePhotosAsync(string photosetId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.removePhotos");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.reorderPhotos

		public async Task PhotosetsReorderPhotosAsync(string photosetId, IEnumerable<string> photoIds) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.reorderPhotos");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_ids", photoIds == null ? String.Empty : String.Join(",", photoIds.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photosets.setPrimaryPhoto

		public async Task PhotosetsSetPrimaryPhotoAsync(string photosetId, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photosets.setPrimaryPhoto");
			dictionary.Add("photoset_id", photosetId);
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.geo.batchCorrectLocation
		/// <summary>
		/// Correct the places hierarchy for all the photos for a user at a given latitude, longitude and accuracy.
		/// </summary>
		/// <remarks>
		/// One of placeId and woeId must be provided.
		/// Batch corrections are processed in a delayed queue so it may take a few minutes before the changes are reflected in a user's photos.
		/// </remarks>
		/// <param name="latitude">The latitude of the photos to be update whose valid range is -90 to 90. Anything more than 6 decimal places will be truncated.</param>
		/// <param name="longitude">The longitude of the photos to be updated whose valid range is -180 to 180. Anything more than 6 decimal places will be truncated.</param>
		/// <param name="accuracy">Recorded accuracy level of the photos to be updated. World level is 1, Country is ~3, Region ~6, City ~11, Street ~16. Current range is 1-16. Defaults to 16 if not specified.</param>
		/// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		/// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		public async Task PhotosGeoBatchCorrectLocationAsync(double latitude, double longitude, GeoAccuracy accuracy = GeoAccuracy.None, string placeId = null, string woeId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.batchCorrectLocation");
			dictionary.Add("latitude", latitude.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("longitude", longitude.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.geo.correctLocation

		public async Task PhotosGeoCorrectLocationAsync(string photoId, string placeId = null, string woeId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.correctLocation");
			dictionary.Add("photo_id", photoId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.geo.getLocation

		public async Task<PlaceInfo> PhotosGeoGetLocationAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.getLocation");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<PlaceInfo>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.getPerms

		public async Task<GeoPermissions> PhotosGeoGetPermsAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.getPerms");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<GeoPermissions>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.photosForLocation

		public async Task<PhotoCollection> PhotosGeoPhotosForLocationAsync(double lat, double lon, GeoAccuracy accuracy = GeoAccuracy.None, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.photosForLocation");
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("perPage", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.geo.removeLocation

		public async Task PhotosGeoRemoveLocationAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.removeLocation");
			dictionary.Add("photo_id", photoId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.geo.setContext

		public async Task PhotosGeoSetContextAsync(string photoId, GeoContext context) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.setContext");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("context", context.ToString("d"));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.geo.setLocation

		public async Task PhotosGeoSetLocationAsync(string photoId, double lat, double lon, GeoAccuracy accuracy = GeoAccuracy.None, GeoContext context = GeoContext.NotDefined) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.setLocation");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			if (context != GeoContext.NotDefined) dictionary.Add("context", context.ToString("d"));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.geo.setPerms

		public async Task PhotosGeoSetPermsAsync(string photoId, bool isPublic, bool isContact, bool isFamily, bool isFriend) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.geo.setPerms");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("is_public", isPublic ? "1" : "0");
			dictionary.Add("is_contact", isContact ? "1" : "0");
			dictionary.Add("is_family", isFamily ? "1" : "0");
			dictionary.Add("is_friend", isFriend ? "1" : "0");
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.getAllContexts

		public async Task<AllContexts> PhotosGetAllContextsAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getAllContexts");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<AllContexts>(dictionary);
		}
		#endregion

		#region flickr.photos.getContactsPhotos

		public async Task<PhotoCollection> PhotosGetContactsPhotosAsync(int? count = null, bool? justFriends = null, bool? singlePhoto = null, bool? includeSelf = null, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getContactsPhotos");
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			if (justFriends != null) dictionary.Add("just_friends", justFriends.Value ? "1" : "0");
			if (singlePhoto != null) dictionary.Add("single_photo", singlePhoto.Value ? "1" : "0");
			if (includeSelf != null) dictionary.Add("include_self", includeSelf.Value ? "1" : "0");
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getContactsPublicPhotos

		public async Task<PhotoCollection> PhotosGetContactsPublicPhotosAsync(string userId, int? count = null, bool? justFriends = null, bool? singlePhoto = null, bool? includeSelf = null, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getContactsPublicPhotos");
			dictionary.Add("user_id", userId);
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			if (justFriends != null) dictionary.Add("just_friends", justFriends.Value ? "1" : "0");
			if (singlePhoto != null) dictionary.Add("single_photo", singlePhoto.Value ? "1" : "0");
			if (includeSelf != null) dictionary.Add("include_self", includeSelf.Value ? "1" : "0");
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getContext

		public async Task<Context> PhotosGetContextAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getContext");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<Context>(dictionary);
		}
		#endregion

		#region flickr.photos.getCounts

		public async Task<PhotoCountCollection> PhotosGetCountsAsync(IEnumerable<DateTime> dates = null, IEnumerable<DateTime> takenDates = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getCounts");
			if (dates != null) dictionary.Add("dates", dates == null ? String.Empty : String.Join(",", dates.Select(d => d.ToUnixTimestamp()).ToArray()));
			if (takenDates != null) dictionary.Add("taken_dates", takenDates == null ? String.Empty : String.Join(",", takenDates.Select(d => d.ToUnixTimestamp()).ToArray()));
			return await GetResponseAsync<PhotoCountCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getExif

		public async Task<ExifTagCollection> PhotosGetExifAsync(string photoId, string secret = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getExif");
			dictionary.Add("photo_id", photoId);
			if (secret != null) dictionary.Add("secret", secret);
			return await GetResponseAsync<ExifTagCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getFavorites

		public async Task<PhotoFavoriteCollection> PhotosGetFavoritesAsync(string photoId, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getFavorites");
			dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoFavoriteCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getInfo

		public async Task<PhotoInfo> PhotosGetInfoAsync(string photoId, string secret = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getInfo");
			dictionary.Add("photo_id", photoId);
			if (secret != null) dictionary.Add("secret", secret);
			return await GetResponseAsync<PhotoInfo>(dictionary);
		}
		#endregion

		#region flickr.photos.getNotInSet

		public async Task<PhotoCollection> PhotosGetNotInSetAsync(int page = 0, int perPage = 0, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getNotInSet");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getNotInSet

		public async Task<PhotoCollection> PhotosGetNotInSetAsync(PartialSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getNotInSet");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getPerms

		public async Task<PhotoPermissions> PhotosGetPermsAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getPerms");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<PhotoPermissions>(dictionary);
		}
		#endregion

		#region flickr.photos.getRecent

		public async Task<PhotoCollection> PhotosGetRecentAsync(int page = 0, int perPage = 0, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getRecent");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getSizes

		public async Task<SizeCollection> PhotosGetSizesAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getSizes");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<SizeCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getUntagged

		public async Task<PhotoCollection> PhotosGetUntaggedAsync(PartialSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getUntagged");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getUntagged

		public async Task<PhotoCollection> PhotosGetUntaggedAsync(int page = 0, int perPage = 0, PhotoSearchExtras extras = PhotoSearchExtras.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getUntagged");
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getWithGeoData

		public async Task<PhotoCollection> PhotosGetWithGeoDataAsync(PartialSearchOptions options = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getWithGeoData");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.getWithoutGeoData

		public async Task<PhotoCollection> PhotosGetWithoutGeoDataAsync(PartialSearchOptions options = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.getWithoutGeoData");
			UtilityMethods.PartialOptionsIntoArray(options, dictionary);
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.licenses.setLicense

		public async Task PhotosLicensesSetLicenseAsync(string photoId, LicenseType licenseId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.licenses.setLicense");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("license_id", licenseId.ToString("d"));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.licenses.getInfo

		public async Task<LicenseCollection> PhotosLicensesGetInfoAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.licenses.getInfo");
			return await GetResponseAsync<LicenseCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.notes.add

		public async Task<string> PhotosNotesAddAsync(string photoId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.notes.add");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("note_x", noteX.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_y", noteY.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_width", noteWidth.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_height", noteHeight.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_text", noteText);
			var result = await GetResponseAsync<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.photos.notes.delete

		public async Task PhotosNotesDeleteAsync(string noteId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.notes.delete");
			dictionary.Add("note_id", noteId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.notes.edit

		public async Task PhotosNotesEditAsync(string noteId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.notes.edit");
			dictionary.Add("note_id", noteId);
			dictionary.Add("note_x", noteX.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_y", noteY.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_width", noteWidth.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_height", noteHeight.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("note_text", noteText);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.people.add

		public async Task PhotosPeopleAddAsync(string photoId, string userId, int? personX = null, int? personY = null, int? personWidth = null, int? personHeight = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.add");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			if (personX != null) dictionary.Add("person_x", personX.ToString().ToLower());
			if (personY != null) dictionary.Add("person_y", personY.ToString().ToLower());
			if (personWidth != null) dictionary.Add("person_width", personWidth.ToString().ToLower());
			if (personHeight != null) dictionary.Add("person_height", personHeight.ToString().ToLower());
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.people.delete

		public async Task PhotosPeopleDeleteAsync(string photoId, string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.delete");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.people.deleteCoords

		public async Task PhotosPeopleDeleteCoordsAsync(string photoId, string userId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.deleteCoords");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.people.editCoords

		public async Task PhotosPeopleEditCoordsAsync(string photoId, string userId, int? personX = null, int? personY = null, int? personWidth = null, int? personHeight = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.editCoords");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("user_id", userId);
			if (personX != null) dictionary.Add("person_x", personX.ToString().ToLower());
			if (personY != null) dictionary.Add("person_y", personY.ToString().ToLower());
			if (personWidth != null) dictionary.Add("person_width", personWidth.ToString().ToLower());
			if (personHeight != null) dictionary.Add("person_height", personHeight.ToString().ToLower());
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.people.getList

		public async Task<PhotoPersonCollection> PhotosPeopleGetListAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.people.getList");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<PhotoPersonCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.recentlyUpdated

		public async Task<PhotoCollection> PhotosRecentlyUpdatedAsync(DateTime minDate, PhotoSearchExtras extras = PhotoSearchExtras.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.recentlyUpdated");
			dictionary.Add("min_date", minDate.ToUnixTimestamp());
			if (extras != PhotoSearchExtras.None) dictionary.Add("extras", UtilityMethods.ExtrasToString(extras));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.removeTag

		public async Task PhotosRemoveTagAsync(string tagId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.removeTag");
			dictionary.Add("tag_id", tagId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.search

		public async Task<PhotoCollection> PhotosSearchAsync(PhotoSearchOptions options) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.search");
			options.AddToDictionary(dictionary);
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.setContentType

		public async Task PhotosSetContentTypeAsync(string photoId, ContentType contentType) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setContentType");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("content_type", contentType.ToString().ToLower());
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.setDates

		public async Task PhotosSetDatesAsync(string photoId, DateTime? datePosted = null, DateTime? dateTaken = null, DateGranularity granularity = DateGranularity.FullDate) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setDates");
			dictionary.Add("photo_id", photoId);
			if (datePosted != null) dictionary.Add("date_posted", datePosted.Value.ToUnixTimestamp());
			if (dateTaken != null) dictionary.Add("date_taken", dateTaken.Value.ToUnixTimestamp());
			if (granularity != DateGranularity.FullDate) dictionary.Add("granularity", granularity.ToString().ToLower());
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.setMeta

		public async Task PhotosSetMetaAsync(string photoId, string title, string description) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setMeta");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("title", title);
			dictionary.Add("description", description);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.setPerms

		public async Task PhotosSetPermsAsync(string photoId, bool isPublic, bool isFriend, bool isFamily, PermissionComment permComments, PermissionAddMeta permAddMeta) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setPerms");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("is_public", isPublic ? "1" : "0");
			dictionary.Add("is_friend", isFriend ? "1" : "0");
			dictionary.Add("is_family", isFamily ? "1" : "0");
			dictionary.Add("perm_comments", permComments.ToString().ToLower());
			dictionary.Add("perm_add_meta", permAddMeta.ToString().ToLower());
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.setSafetyLevel

		public async Task PhotosSetSafetyLevelAsync(string photoId, SafetyLevel safetyLevel = SafetyLevel.None, HiddenFromSearch hidden = HiddenFromSearch.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setSafetyLevel");
			dictionary.Add("photo_id", photoId);
			if (safetyLevel != SafetyLevel.None) dictionary.Add("safety_level", safetyLevel.ToString("d"));
			if (hidden != HiddenFromSearch.None) dictionary.Add("hidden", hidden.ToString().ToLower());
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.setTags

		public async Task PhotosSetTagsAsync(string photoId, IEnumerable<string> tags) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.setTags");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("tags", tags == null ? String.Empty : String.Join(",", tags.ToArray()));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.approveSuggestion

		public async Task PhotosSuggestionsApproveSuggestionAsync(string suggestionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.approveSuggestion");
			dictionary.Add("suggestion_id", suggestionId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.getList

		public async Task<SuggestionCollection> PhotosSuggestionsGetListAsync(string photoId, SuggestionStatus status) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.getList");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("status", status.ToString().ToLower());
			return await GetResponseAsync<SuggestionCollection>(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.rejectSuggestion

		public async Task PhotosSuggestionsRejectSuggestionAsync(string suggestionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.rejectSuggestion");
			dictionary.Add("suggestion_id", suggestionId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.removeSuggestion

		public async Task PhotosSuggestionsRemoveSuggestionAsync(string suggestionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.suggestions.removeSuggestion");
			dictionary.Add("suggestion_id", suggestionId);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.suggestions.suggestLocation

		public async Task PhotosSuggestionsSuggestLocationAsync(string photoId, double lat, double lon, GeoAccuracy accuracy = GeoAccuracy.None, string woeId = null, string placeId = null, string note = null) 
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
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.transformRotate

		public async Task PhotosTransformRotateAsync(string photoId, int degrees) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.transformRotate");
			dictionary.Add("photo_id", photoId);
			dictionary.Add("degrees", degrees.ToString(CultureInfo.InvariantCulture));
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.photos.upload.checkTickets

		public async Task<TicketCollection> PhotosUploadCheckTicketsAsync(IEnumerable<string> tickets) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.photos.upload.checkTickets");
			dictionary.Add("tickets", tickets == null ? String.Empty : String.Join(",", tickets.ToArray()));
			return await GetResponseAsync<TicketCollection>(dictionary);
		}
		#endregion

		#region flickr.places.find
		/// <summary>
		/// Return a list of place IDs for a query string.&lt;br /&gt;&lt;br /&gt;
		/// The flickr.places.find method is &lt;b&gt;not&lt;/b&gt; a geocoder. It will round &lt;q&gt;up&lt;/q&gt; to the nearest place type to which place IDs apply. For example, if you pass it a street level address it will return the city that contains the address rather than the street, or building, itself.
		/// </summary>
		/// <param name="query">The query string to use for place ID lookups</param>
		/// <!--<param name="bbox">A bounding box for limiting the area to query.</param>
		/// <param name="extras">Secret sauce.</param>
		/// <param name="safe">Do we want sexy time words in our venue results?</param>-->
		public async Task<PlaceCollection> PlacesFindAsync(string query) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.find");
			dictionary.Add("query", query);
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.findByLatLon
		/// <summary>
		/// Return a place ID for a latitude, longitude and accuracy triple.&lt;br /&gt;&lt;br /&gt;
		/// The flickr.places.findByLatLon method is not meant to be a (reverse) geocoder in the traditional sense. It is designed to allow users to find photos for "places" and will round up to the nearest place type to which corresponding place IDs apply.&lt;br /&gt;&lt;br /&gt;
		/// For example, if you pass it a street level coordinate it will return the city that contains the point rather than the street, or building, itself.&lt;br /&gt;&lt;br /&gt;
		/// It will also truncate latitudes and longitudes to three decimal points.
		/// </summary>
		/// <param name="lat">The latitude whose valid range is -90 to 90. Anything more than 4 decimal places will be truncated.</param>
		/// <param name="lon">The longitude whose valid range is -180 to 180. Anything more than 4 decimal places will be truncated.</param>
		/// <param name="accuracy">Recorded accuracy level of the location information. World level is 1, Country is ~3, Region ~6, City ~11, Street ~16. Current range is 1-16. The default is 16.</param>
		public async Task<PlaceCollection> PlacesFindByLatLonAsync(double lat, double lon, GeoAccuracy accuracy = GeoAccuracy.None) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.findByLatLon");
			dictionary.Add("lat", lat.ToString(NumberFormatInfo.InvariantInfo));
			dictionary.Add("lon", lon.ToString(NumberFormatInfo.InvariantInfo));
			if (accuracy != GeoAccuracy.None) dictionary.Add("accuracy", accuracy.ToString("d"));
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getChildrenWithPhotosPublic
		/// <summary>Return a list of locations with public photos that are parented by a Where on Earth (WOE) or Places ID.</summary>
		/// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		/// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		public async Task<PlaceCollection> PlacesGetChildrenWithPhotosPublicAsync(string placeId = null, string woeId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getChildrenWithPhotosPublic");
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getInfo
		/// <summary>Get informations about a place.</summary>
		/// <param name="placeId">A Flickr Places ID. &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;</param>
		/// <param name="woeId">A Where On Earth (WOE) ID. &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;</param>
		public async Task<PlaceInfo> PlacesGetInfoAsync(string placeId = null, string woeId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getInfo");
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return await GetResponseAsync<PlaceInfo>(dictionary);
		}
		#endregion

		#region flickr.places.getInfoByUrl
		/// <summary>Lookup information about a place, by its flickr.com/places URL.</summary>
		/// <param name="url">A flickr.com/places URL in the form of /country/region/city. For example: /Canada/Quebec/Montreal</param>
		public async Task<PlaceInfo> PlacesGetInfoByUrlAsync(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getInfoByUrl");
			dictionary.Add("url", url);
			return await GetResponseAsync<PlaceInfo>(dictionary);
		}
		#endregion

		#region flickr.places.getPlaceTypes
		/// <summary>Fetches a list of available place types for Flickr.</summary>
		public async Task<PlaceTypeInfoCollection> PlacesGetPlaceTypesAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getPlaceTypes");
			return await GetResponseAsync<PlaceTypeInfoCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getShapeHistory
		/// <summary>Return an historical list of all the shape data generated for a Places or Where on Earth (WOE) ID.</summary>
		/// <param name="placeId">A Flickr Places ID. &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;</param>
		/// <param name="woeId">A Where On Earth (WOE) ID. &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;</param>
		public async Task<ShapeDataCollection> PlacesGetShapeHistoryAsync(string placeId = null, string woeId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getShapeHistory");
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return await GetResponseAsync<ShapeDataCollection>(dictionary);
		}
		#endregion

		#region flickr.places.getTopPlacesList
		/// <summary>Return the top 100 most geotagged places for a day.</summary>
		/// <param name="placeTypeId">
		/// The numeric ID for a specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place type IDs are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;22&lt;/strong&gt;: neighbourhood&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;7&lt;/strong&gt;: locality&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;8&lt;/strong&gt;: region&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;12&lt;/strong&gt;: country&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;29&lt;/strong&gt;: continent&lt;/li&gt;
		/// &lt;/ul&gt;
		/// </param>
		/// <param name="date">A valid date in YYYY-MM-DD format. The default is yesterday.</param>
		/// <param name="woeId">Limit your query to only those top places belonging to a specific Where on Earth (WOE) identifier.</param>
		/// <param name="placeId">Limit your query to only those top places belonging to a specific Flickr Places identifier.</param>
		public async Task<PlaceCollection> PlacesGetTopPlacesListAsync(PlaceType placeTypeId, DateTime? date = null, string placeId = null, string woeId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.getTopPlacesList");
			dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (date != null) dictionary.Add("date", date.Value.ToUnixTimestamp());
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (woeId != null) dictionary.Add("woe_id", woeId);
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.placesForBoundingBox
		/// <summary>
		/// Return all the locations of a matching place type for a bounding box.&lt;br /&gt;&lt;br /&gt;
		/// The maximum allowable size of a bounding box (the distance between the SW and NE corners) is governed by the place type you are requesting. Allowable sizes are as follows:
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;neighbourhood&lt;/strong&gt;: 3km (1.8mi)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;locality&lt;/strong&gt;: 7km (4.3mi)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;county&lt;/strong&gt;: 50km (31mi)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;region&lt;/strong&gt;: 200km (124mi)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;country&lt;/strong&gt;: 500km (310mi)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;continent&lt;/strong&gt;: 1500km (932mi)&lt;/li&gt;
		/// &lt;/ul&gt;
		/// </summary>
		/// <param name="bbox">A comma-delimited list of 4 values defining the Bounding Box of the area that will be searched. The 4 values represent the bottom-left corner of the box and the top-right corner, minimum_longitude, minimum_latitude, maximum_longitude, maximum_latitude.</param>
		/// <param name="placeType">
		/// The name of place type to using as the starting point to search for places in a bounding box. Valid placetypes are:
		/// &lt;ul&gt;
		/// &lt;li&gt;neighbourhood&lt;/li&gt;
		/// &lt;li&gt;locality&lt;/li&gt;
		/// &lt;li&gt;county&lt;/li&gt;
		/// &lt;li&gt;region&lt;/li&gt;
		/// &lt;li&gt;country&lt;/li&gt;
		/// &lt;li&gt;continent&lt;/li&gt;
		/// &lt;/ul&gt;
		/// &lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;The "place_type" argument has been deprecated in favor of the "place_type_id" argument. It won't go away but it will not be added to new methods. A complete list of place type IDs is available using the &lt;a href="http://www.flickr.com/services/api/flickr.places.getPlaceTypes.html"&gt;flickr.places.getPlaceTypes&lt;/a&gt; method. (While optional, you must pass either a valid place type or place type ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeTypeId">
		/// The numeric ID for a specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place type IDs are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;22&lt;/strong&gt;: neighbourhood&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;7&lt;/strong&gt;: locality&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;8&lt;/strong&gt;: region&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;12&lt;/strong&gt;: country&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;29&lt;/strong&gt;: continent&lt;/li&gt;
		/// &lt;/ul&gt;
		/// &lt;br /&gt;&lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid place type or place type ID.)&lt;/span&gt;
		/// </param>
		/// <param name="recursive">
		/// Perform a recursive place type search. For example, if you search for neighbourhoods in a given bounding box but there are no results the method will also query for localities and so on until one or more valid places are found.&lt;br /&lt;br /&gt;
		/// Recursive searches do not change the bounding box size restrictions for the initial place type passed to the method.
		/// </param>
		public async Task<PlaceCollection> PlacesPlacesForBoundingBoxAsync(BoundaryBox bbox, PlaceType placeType = PlaceType.None, int? placeTypeId = null, bool? recursive = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.placesForBoundingBox");
			dictionary.Add("bbox", bbox.ToString().ToLower());
			if (placeType != PlaceType.None) dictionary.Add("place_type", placeType.ToString().ToLower());
			if (placeTypeId != null) dictionary.Add("place_type_id", placeTypeId.ToString().ToLower());
			if (recursive != null) dictionary.Add("recursive", recursive.Value ? "1" : "0");
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.placesForContacts
		/// <summary>Return a list of the top 100 unique places clustered by a given placetype for a user's contacts. </summary>
		/// <param name="placeType">
		/// A specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place types are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;neighbourhood&lt;/strong&gt; (and neighborhood)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;locality&lt;/strong&gt;&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;region&lt;/strong&gt;&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;country&lt;/strong&gt;&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;continent&lt;/strong&gt;&lt;/li&gt;
		/// &lt;/ul&gt;
		/// &lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;The "place_type" argument has been deprecated in favor of the "place_type_id" argument. It won't go away but it will not be added to new methods. A complete list of place type IDs is available using the &lt;a href="http://www.flickr.com/services/api/flickr.places.getPlaceTypes.html"&gt;flickr.places.getPlaceTypes&lt;/a&gt; method. (While optional, you must pass either a valid place type or place type ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeTypeId">
		/// The numeric ID for a specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place type IDs are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;22&lt;/strong&gt;: neighbourhood&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;7&lt;/strong&gt;: locality&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;8&lt;/strong&gt;: region&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;12&lt;/strong&gt;: country&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;29&lt;/strong&gt;: continent&lt;/li&gt;
		/// &lt;/ul&gt;
		/// &lt;br /&gt;&lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid place type or place type ID.)&lt;/span&gt;
		/// </param>
		/// <param name="woeId">
		/// A Where on Earth identifier to use to filter photo clusters. For example all the photos clustered by &lt;strong&gt;locality&lt;/strong&gt; in the United States (WOE ID &lt;strong&gt;23424977&lt;/strong&gt;).&lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeId">
		/// A Flickr Places identifier to use to filter photo clusters. For example all the photos clustered by &lt;strong&gt;locality&lt;/strong&gt; in the United States (Place ID &lt;strong&gt;4KO02SibApitvSBieQ&lt;/strong&gt;).
		/// &lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="threshold">
		/// The minimum number of photos that a place type must have to be included. If the number of photos is lowered then the parent place type for that place will be used.&lt;br /&gt;&lt;br /&gt;
		/// For example if your contacts only have &lt;strong&gt;3&lt;/strong&gt; photos taken in the locality of Montreal&lt;/strong&gt; (WOE ID 3534) but your threshold is set to &lt;strong&gt;5&lt;/strong&gt; then those photos will be "rolled up" and included instead with a place record for the region of Quebec (WOE ID 2344924).
		/// </param>
		/// <param name="contacts">Search your contacts. Either 'all' or 'ff' for just friends and family. (Default is all)</param>
		/// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		/// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		public async Task<PlaceCollection> PlacesPlacesForContactsAsync(PlaceType placeType = PlaceType.None, int? placeTypeId = null, string placeId = null, string woeId = null, int? threshold = null, string contacts = null, DateTime? minUploadDate = null, DateTime? maxUploadDate = null, DateTime? minTakenDate = null, DateTime? maxTakenDate = null) 
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
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.placesForTags
		/// <summary>Return a list of the top 100 unique places clustered by a given placetype for set of tags or machine tags. </summary>
		/// <param name="placeTypeId">
		/// The numeric ID for a specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place type IDs are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;22&lt;/strong&gt;: neighbourhood&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;7&lt;/strong&gt;: locality&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;8&lt;/strong&gt;: region&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;12&lt;/strong&gt;: country&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;29&lt;/strong&gt;: continent&lt;/li&gt;
		/// &lt;/ul&gt;
		/// </param>
		/// <param name="woeId">
		/// A Where on Earth identifier to use to filter photo clusters. For example all the photos clustered by &lt;strong&gt;locality&lt;/strong&gt; in the United States (WOE ID &lt;strong&gt;23424977&lt;/strong&gt;).
		/// &lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeId">
		/// A Flickr Places identifier to use to filter photo clusters. For example all the photos clustered by &lt;strong&gt;locality&lt;/strong&gt; in the United States (Place ID &lt;strong&gt;4KO02SibApitvSBieQ&lt;/strong&gt;).
		/// &lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="threshold">
		/// The minimum number of photos that a place type must have to be included. If the number of photos is lowered then the parent place type for that place will be used.&lt;br /&gt;&lt;br /&gt;
		/// For example if you only have &lt;strong&gt;3&lt;/strong&gt; photos taken in the locality of Montreal&lt;/strong&gt; (WOE ID 3534) but your threshold is set to &lt;strong&gt;5&lt;/strong&gt; then those photos will be "rolled up" and included instead with a place record for the region of Quebec (WOE ID 2344924).
		/// </param>
		/// <param name="tags">A comma-delimited list of tags. Photos with one or more of the tags listed will be returned.</param>
		/// <param name="tagMode">Either 'any' for an OR combination of tags, or 'all' for an AND combination. Defaults to 'any' if not specified.</param>
		/// <param name="machineTags">
		/// Aside from passing in a fully formed machine tag, there is a special syntax for searching on specific properties :
		/// &lt;ul&gt;
		/// &lt;li&gt;Find photos using the 'dc' namespace :    &lt;code&gt;"machine_tags" =&gt; "dc:"&lt;/code&gt;&lt;/li&gt;
		/// &lt;li&gt; Find photos with a title in the 'dc' namespace : &lt;code&gt;"machine_tags" =&gt; "dc:title="&lt;/code&gt;&lt;/li&gt;
		/// &lt;li&gt;Find photos titled "mr. camera" in the 'dc' namespace : &lt;code&gt;"machine_tags" =&gt; "dc:title=\"mr. camera\"&lt;/code&gt;&lt;/li&gt;
		/// &lt;li&gt;Find photos whose value is "mr. camera" : &lt;code&gt;"machine_tags" =&gt; "*:*=\"mr. camera\""&lt;/code&gt;&lt;/li&gt;
		/// &lt;li&gt;Find photos that have a title, in any namespace : &lt;code&gt;"machine_tags" =&gt; "*:title="&lt;/code&gt;&lt;/li&gt;
		/// &lt;li&gt;Find photos that have a title, in any namespace, whose value is "mr. camera" : &lt;code&gt;"machine_tags" =&gt; "*:title=\"mr. camera\""&lt;/code&gt;&lt;/li&gt;
		/// &lt;li&gt;Find photos, in the 'dc' namespace whose value is "mr. camera" : &lt;code&gt;"machine_tags" =&gt; "dc:*=\"mr. camera\""&lt;/code&gt;&lt;/li&gt;
		/// &lt;/ul&gt;
		/// Multiple machine tags may be queried by passing a comma-separated list. The number of machine tags you can pass in a single query depends on the tag mode (AND or OR) that you are querying with. "AND" queries are limited to (16) machine tags. "OR" queries are limited
		/// to (8).
		/// </param>
		/// <param name="machineTagMode">Either 'any' for an OR combination of tags, or 'all' for an AND combination. Defaults to 'any' if not specified.</param>
		/// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		/// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		public async Task<PlaceCollection> PlacesPlacesForTagsAsync(PlaceType placeTypeId = PlaceType.None, string woeId = null, string placeId = null, int? threshold = null, IEnumerable<string> tags = null, TagMode tagMode = TagMode.None, IEnumerable<string> machineTags = null, MachineTagMode machineTagMode = MachineTagMode.None, DateTime? minUploadDate = null, DateTime? maxUploadDate = null, DateTime? minTakenDate = null, DateTime? maxTakenDate = null) 
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
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.placesForUser
		/// <summary>Return a list of the top 100 unique places clustered by a given placetype for a user. </summary>
		/// <param name="placeTypeId">
		/// The numeric ID for a specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place type IDs are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;22&lt;/strong&gt;: neighbourhood&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;7&lt;/strong&gt;: locality&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;8&lt;/strong&gt;: region&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;12&lt;/strong&gt;: country&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;29&lt;/strong&gt;: continent&lt;/li&gt;
		/// &lt;/ul&gt;
		/// &lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;The "place_type" argument has been deprecated in favor of the "place_type_id" argument. It won't go away but it will not be added to new methods. A complete list of place type IDs is available using the &lt;a href="http://www.flickr.com/services/api/flickr.places.getPlaceTypes.html"&gt;flickr.places.getPlaceTypes&lt;/a&gt; method. (While optional, you must pass either a valid place type or place type ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeType">
		/// A specific place type to cluster photos by. &lt;br /&gt;&lt;br /&gt;
		/// Valid place types are :
		/// &lt;ul&gt;
		/// &lt;li&gt;&lt;strong&gt;neighbourhood&lt;/strong&gt; (and neighborhood)&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;locality&lt;/strong&gt;&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;region&lt;/strong&gt;&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;country&lt;/strong&gt;&lt;/li&gt;
		/// &lt;li&gt;&lt;strong&gt;continent&lt;/strong&gt;&lt;/li&gt;
		/// &lt;/ul&gt;
		/// &lt;br /&gt;&lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid place type or place type ID.)&lt;/span&gt;
		/// </param>
		/// <param name="woeId">
		/// A Where on Earth identifier to use to filter photo clusters. For example all the photos clustered by &lt;strong&gt;locality&lt;/strong&gt; in the United States (WOE ID &lt;strong&gt;23424977&lt;/strong&gt;).&lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeId">
		/// A Flickr Places identifier to use to filter photo clusters. For example all the photos clustered by &lt;strong&gt;locality&lt;/strong&gt; in the United States (Place ID &lt;strong&gt;4KO02SibApitvSBieQ&lt;/strong&gt;).&lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="threshold">
		/// The minimum number of photos that a place type must have to be included. If the number of photos is lowered then the parent place type for that place will be used.&lt;br /&gt;&lt;br /&gt;
		/// For example if you only have &lt;strong&gt;3&lt;/strong&gt; photos taken in the locality of Montreal&lt;/strong&gt; (WOE ID 3534) but your threshold is set to &lt;strong&gt;5&lt;/strong&gt; then those photos will be "rolled up" and included instead with a place record for the region of Quebec (WOE ID 2344924).
		/// </param>
		/// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		/// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		public async Task<PlaceCollection> PlacesPlacesForUserAsync(PlaceType placeTypeId = PlaceType.None, string placeType = null, string woeId = null, string placeId = null, int? threshold = null, DateTime? minUploadDate = null, DateTime? maxUploadDate = null, DateTime? minTakenDate = null, DateTime? maxTakenDate = null) 
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
			return await GetResponseAsync<PlaceCollection>(dictionary);
		}
		#endregion

		#region flickr.places.tagsForPlace
		/// <summary>Return a list of the top 100 unique tags for a Flickr Places or Where on Earth (WOE) ID</summary>
		/// <param name="woeId">
		/// A Where on Earth identifier to use to filter photo clusters.&lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="placeId">
		/// A Flickr Places identifier to use to filter photo clusters.&lt;br /&gt;&lt;br /&gt;
		/// &lt;span style="font-style:italic;"&gt;(While optional, you must pass either a valid Places ID or a WOE ID.)&lt;/span&gt;
		/// </param>
		/// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned. The date should be in the form of a unix timestamp.</param>
		/// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		/// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned. The date should be in the form of a mysql datetime.</param>
		public async Task<TagCollection> PlacesTagsForPlaceAsync(string woeId = null, string placeId = null, DateTime? minUploadDate = null, DateTime? maxUploadDate = null, DateTime? minTakenDate = null, DateTime? maxTakenDate = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.places.tagsForPlace");
			if (woeId != null) dictionary.Add("woe_id", woeId);
			if (placeId != null) dictionary.Add("place_id", placeId);
			if (minUploadDate != null) dictionary.Add("min_upload_date", minUploadDate.Value.ToUnixTimestamp());
			if (maxUploadDate != null) dictionary.Add("max_upload_date", maxUploadDate.Value.ToUnixTimestamp());
			if (minTakenDate != null) dictionary.Add("min_taken_date", minTakenDate.Value.ToUnixTimestamp());
			if (maxTakenDate != null) dictionary.Add("max_taken_date", maxTakenDate.Value.ToUnixTimestamp());
			return await GetResponseAsync<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.prefs.getContentType
		/// <summary>Returns the default content type preference for the user.</summary>
		public async Task<ContentType> PrefsGetContentTypeAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getContentType");
			var result = await GetResponseAsync<UnknownResponse>(dictionary);
			return result.GetAttributeValue<ContentType>("person", "content_type");
		}
		#endregion

		#region flickr.prefs.getGeoPerms
		/// <summary>
		/// Returns the default privacy level for geographic information attached to the user's photos and whether or not the user has chosen to use geo-related EXIF information to automatically geotag their photos.
		/// Possible values, for viewing geotagged photos, are:
		/// &lt;ul&gt;
		/// &lt;li&gt;0 : &lt;i&gt;No default set&lt;/i&gt;&lt;/li&gt;
		/// &lt;li&gt;1 : Public&lt;/li&gt;
		/// &lt;li&gt;2 : Contacts only&lt;/li&gt;
		/// &lt;li&gt;3 : Friends and Family only&lt;/li&gt;
		/// &lt;li&gt;4 : Friends only&lt;/li&gt;
		/// &lt;li&gt;5 : Family only&lt;/li&gt;
		/// &lt;li&gt;6 : Private&lt;/li&gt;
		/// &lt;/ul&gt;
		/// Users can edit this preference at &lt;a href="http://www.flickr.com/account/geo/privacy/"&gt;http://www.flickr.com/account/geo/privacy/&lt;/a&gt;.
		/// &lt;br /&gt;&lt;br /&gt;
		/// Possible values for whether or not geo-related EXIF information will be used to geotag a photo are:
		/// &lt;ul&gt;
		/// &lt;li&gt;0: Geo-related EXIF information will be ignored&lt;/li&gt;
		/// &lt;li&gt;1: Geo-related EXIF information will be used to try and geotag photos on upload&lt;/li&gt;
		/// &lt;/ul&gt;
		/// Users can edit this preference at &lt;a href="http://www.flickr.com/account/geo/exif/?from=privacy"&gt;http://www.flickr.com/account/geo/exif/?from=privacy&lt;/a&gt;
		/// </summary>
		public async Task<UserGeoPermissions> PrefsGetGeoPermsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getGeoPerms");
			return await GetResponseAsync<UserGeoPermissions>(dictionary);
		}
		#endregion

		#region flickr.prefs.getHidden
		/// <summary>Returns the default hidden preference for the user.</summary>
		public async Task<HiddenFromSearch> PrefsGetHiddenAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getHidden");
			var result = await GetResponseAsync<UnknownResponse>(dictionary);
			return result.GetAttributeValue<HiddenFromSearch>("person", "hidden");
		}
		#endregion

		#region flickr.prefs.getPrivacy
		/// <summary>
		/// Returns the default privacy level preference for the user.
		/// Possible values are:
		/// &lt;ul&gt;
		/// &lt;li&gt;1 : Public&lt;/li&gt;
		/// &lt;li&gt;2 : Friends only&lt;/li&gt;
		/// &lt;li&gt;3 : Family only&lt;/li&gt;
		/// &lt;li&gt;4 : Friends and Family&lt;/li&gt;
		/// &lt;li&gt;5 : Private&lt;/li&gt;
		/// &lt;/ul&gt;
		/// </summary>
		public async Task<PrivacyFilter> PrefsGetPrivacyAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getPrivacy");
			var result = await GetResponseAsync<UnknownResponse>(dictionary);
			return result.GetAttributeValue<PrivacyFilter>("person", "privacy");
		}
		#endregion

		#region flickr.prefs.getSafetyLevel
		/// <summary>Returns the default safety level preference for the user.</summary>
		public async Task<SafetyLevel> PrefsGetSafetyLevelAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.prefs.getSafetyLevel");
			var result = await GetResponseAsync<UnknownResponse>(dictionary);
			return result.GetAttributeValue<SafetyLevel>("person", "safety_level");
		}
		#endregion

		#region flickr.push.getSubscriptions
		/// <summary>
		/// Returns a list of the subscriptions for the logged-in user.
		/// &lt;br&gt;&lt;br&gt;
		/// &lt;i&gt;(this method is experimental and may change)&lt;/i&gt;
		/// </summary>
		public async Task<SubscriptionCollection> PushGetSubscriptionsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.getSubscriptions");
			return await GetResponseAsync<SubscriptionCollection>(dictionary);
		}
		#endregion

		#region flickr.push.getTopics
		/// <summary>
		/// All the different flavours of anteater.
		/// &lt;br&gt;&lt;br&gt;
		/// &lt;i&gt;(this method is experimental and may change)&lt;/i&gt;
		/// </summary>
		public async Task<string[]> PushGetTopicsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.getTopics");
			var result = await GetResponseAsync<UnknownResponse>(dictionary);
			return result.GetElementArray<string>("topic", "name");
		}
		#endregion

		#region flickr.push.subscribe
		/// <summary>
		/// In ur pandas, tickling ur unicorn
		/// &lt;br&gt;&lt;br&gt;
		/// &lt;i&gt;(this method is experimental and may change)&lt;/i&gt;
		/// </summary>
		/// <param name="topic">The type of subscription. See &lt;a href="http://www.flickr.com/services/api/flickr.push.getTopics.htm"&gt;flickr.push.getTopics&lt;/a&gt;.</param>
		/// <param name="callback">The url for the subscription endpoint. Limited to 255 bytes, and must be unique for this user, i.e. no two subscriptions for a given user may use the same callback url.</param>
		/// <param name="verify">The verification mode, either &lt;code&gt;sync&lt;/code&gt; or &lt;code&gt;async&lt;/code&gt;. See the &lt;a href="http://pubsubhubbub.googlecode.com/svn/trunk/pubsubhubbub-core-0.3.html#subscribingl"&gt;Google PubSubHubbub spec&lt;/a&gt; for details.</param>
		/// <param name="verifyToken">The verification token to be echoed back to the subscriber during the verification callback, as per the &lt;a href="http://pubsubhubbub.googlecode.com/svn/trunk/pubsubhubbub-core-0.3.html#subscribing"&gt;Google PubSubHubbub spec&lt;/a&gt;. Limited to 200 bytes.</param>
		/// <param name="leaseSeconds">Number of seconds for which the subscription will be valid. Legal values are 60 to 86400 (1 minute to 1 day). If not present, the subscription will be auto-renewing.</param>
		/// <param name="woeIds">
		/// A 32-bit integer for a &lt;a href="http://developer.yahoo.com/geo/geoplanet/"&gt;Where on Earth ID&lt;/a&gt;. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt;.
		/// &lt;br/&gt;&lt;br/&gt;
		/// The order of precedence for geo subscriptions is : woe ids, place ids, radial i.e. the &lt;code&gt;lat, lon&lt;/code&gt; parameters will be ignored if &lt;code&gt;place_ids&lt;/code&gt; is present, which will be ignored if &lt;code&gt;woe_ids&lt;/code&gt; is present.
		/// </param>
		/// <param name="placeIds">
		/// A comma-separated list of Flickr place IDs. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt;.
		/// &lt;br/&gt;&lt;br/&gt;
		/// The order of precedence for geo subscriptions is : woe ids, place ids, radial i.e. the &lt;code&gt;lat, lon&lt;/code&gt; parameters will be ignored if &lt;code&gt;place_ids&lt;/code&gt; is present, which will be ignored if &lt;code&gt;woe_ids&lt;/code&gt; is present.
		/// </param>
		/// <param name="lat">
		/// A latitude value, in decimal format. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt;. Defines the latitude for a radial query centered around (lat, lon).
		/// &lt;br/&gt;&lt;br/&gt;
		/// The order of precedence for geo subscriptions is : woe ids, place ids, radial i.e. the &lt;code&gt;lat, lon&lt;/code&gt; parameters will be ignored if &lt;code&gt;place_ids&lt;/code&gt; is present, which will be ignored if &lt;code&gt;woe_ids&lt;/code&gt; is present.
		/// </param>
		/// <param name="lon">
		/// A longitude value, in decimal format. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt;. Defines the longitude for a radial query centered around (lat, lon).
		/// &lt;br/&gt;&lt;br/&gt;
		/// The order of precedence for geo subscriptions is : woe ids, place ids, radial i.e. the &lt;code&gt;lat, lon&lt;/code&gt; parameters will be ignored if &lt;code&gt;place_ids&lt;/code&gt; is present, which will be ignored if &lt;code&gt;woe_ids&lt;/code&gt; is present.
		/// </param>
		/// <param name="radius">
		/// A radius value, in the units defined by radius_units. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt;. Defines the radius of a circle for a radial query centered around (lat, lon). Default is 5 km.
		/// &lt;br/&gt;&lt;br/&gt;
		/// The order of precedence for geo subscriptions is : woe ids, place ids, radial i.e. the &lt;code&gt;lat, lon&lt;/code&gt; parameters will be ignored if &lt;code&gt;place_ids&lt;/code&gt; is present, which will be ignored if &lt;code&gt;woe_ids&lt;/code&gt; is present.
		/// </param>
		/// <param name="radiusUnits">
		/// Defines the units for the radius parameter. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt;. Options are &lt;code&gt;mi&lt;/code&gt; and &lt;code&gt;km&lt;/code&gt;. Default is &lt;code&gt;km&lt;/code&gt;.
		/// &lt;br/&gt;&lt;br/&gt;
		/// The order of precedence for geo subscriptions is : woe ids, place ids, radial i.e. the &lt;code&gt;lat, lon&lt;/code&gt; parameters will be ignored if &lt;code&gt;place_ids&lt;/code&gt; is present, which will be ignored if &lt;code&gt;woe_ids&lt;/code&gt; is present.
		/// </param>
		/// <param name="accuracy">
		/// Defines the minimum accuracy required for photos to be included in a subscription. Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;geo&lt;/code&gt; Legal values are 1-16, default is 1 (i.e. any accuracy level).
		/// &lt;ul&gt;
		/// &lt;li&gt;World level is 1&lt;/li&gt;
		/// &lt;li&gt;Country is ~3&lt;/li&gt;
		/// &lt;li&gt;Region is ~6&lt;/li&gt;
		/// &lt;li&gt;City is ~11&lt;/li&gt;
		/// &lt;li&gt;Street is ~16&lt;/li&gt;
		/// &lt;/ul&gt;
		/// </param>
		/// <param name="nsids">A comma-separated list of nsids representing Flickr Commons institutions (see &lt;a href="http://www.flickr.com/services/api/flickr.commons.getInstitutions.html"&gt;flickr.commons.getInstitutions&lt;/a&gt;). Only valid if &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;commons&lt;/code&gt;. If not present this argument defaults to all Flickr Commons institutions.</param>
		/// <param name="tags">A comma-separated list of strings to be used for tag subscriptions. Photos with one or more of the tags listed will be included in the subscription. Only valid if the &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;tags&lt;/code&gt;.</param>
		/// <!--<param name="machineTags">A comma-separated list of strings to be used for machine tag subscriptions. Photos with one or more of the machine tags listed will be included in the subscription. Currently the format must be &lt;code&gt;namespace:tag_name=value&lt;/code&gt; Only valid if the &lt;code&gt;topic&lt;/code&gt; is &lt;code&gt;tags&lt;/code&gt;.</param>
		/// <param name="updateType"></param>
		/// <param name="outputFormat"></param>
		/// <param name="mailto"></param>-->
		public async Task PushSubscribeAsync(string topic, string callback, string verify, string verifyToken = null, int? leaseSeconds = null, IEnumerable<int> woeIds = null, IEnumerable<string> placeIds = null, double? lat = null, double? lon = null, int? radius = null, RadiusUnit radiusUnits = RadiusUnit.None, GeoAccuracy accuracy = GeoAccuracy.None, IEnumerable<string> nsids = null, IEnumerable<string> tags = null) 
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
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.push.unsubscribe
		/// <summary>
		/// Why would you want to do this?
		/// &lt;br&gt;&lt;br&gt;
		/// &lt;i&gt;(this method is experimental and may change)&lt;/i&gt;
		/// </summary>
		/// <param name="topic">The type of subscription. See &lt;a href="http://www.flickr.com/services/api/flickr.push.getTopics.htm"&gt;flickr.push.getTopics&lt;/a&gt;.</param>
		/// <param name="callback">The url for the subscription endpoint (must be the same url as was used when creating the subscription).</param>
		/// <param name="verify">The verification mode, either 'sync' or 'async'. See the &lt;a href="http://pubsubhubbub.googlecode.com/svn/trunk/pubsubhubbub-core-0.3.html#subscribingl"&gt;Google PubSubHubbub spec&lt;/a&gt; for details.</param>
		/// <param name="verifyToken">The verification token to be echoed back to the subscriber during the verification callback, as per the &lt;a href="http://pubsubhubbub.googlecode.com/svn/trunk/pubsubhubbub-core-0.3.html#subscribing"&gt;Google PubSubHubbub spec&lt;/a&gt;. Limited to 200 bytes.</param>
		public async Task PushUnsubscribeAsync(string topic, string callback, string verify, string verifyToken = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.push.unsubscribe");
			dictionary.Add("topic", topic);
			dictionary.Add("callback", callback);
			dictionary.Add("verify", verify);
			if (verifyToken != null) dictionary.Add("verify_token", verifyToken);
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.reflection.getMethodInfo
		/// <summary>Returns information for a given flickr API method.</summary>
		/// <param name="methodName">The name of the method to fetch information for.</param>
		public async Task<Method> ReflectionGetMethodInfoAsync(string methodName) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.reflection.getMethodInfo");
			dictionary.Add("method_name", methodName);
			return await GetResponseAsync<Method>(dictionary);
		}
		#endregion

		#region flickr.reflection.getMethods
		/// <summary>Returns a list of available flickr API methods.</summary>
		public async Task<MethodCollection> ReflectionGetMethodsAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.reflection.getMethods");
			return await GetResponseAsync<MethodCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getCollectionDomains
		/// <summary>Get a list of referring domains for a collection</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="collectionId">The id of the collection to get stats for. If not provided, stats for all collections will be returned.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		public async Task<StatDomainCollection> StatsGetCollectionDomainsAsync(DateTime date, string collectionId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCollectionDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (collectionId != null) dictionary.Add("collection_id", collectionId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatDomainCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getCollectionReferrers
		/// <summary>Get a list of referrers from a given domain to a collection</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
		/// <param name="collectionId">The id of the collection to get stats for. If not provided, stats for all collections will be returned.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <param name="perPage">Number of referrers to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		public async Task<StatReferrerCollection> StatsGetCollectionReferrersAsync(DateTime date, string domain, string collectionId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCollectionReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (collectionId != null) dictionary.Add("collection_id", collectionId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatReferrerCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getCollectionStats
		/// <summary>Get the number of views on a collection for a given date.</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="collectionId">The id of the collection to get stats for.</param>
		public async Task<Stats> StatsGetCollectionStatsAsync(DateTime date, string collectionId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCollectionStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("collection_id", collectionId);
			return await GetResponseAsync<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getCSVFiles
		/// <summary>
		/// Returns a list of URLs for text files containing &lt;i&gt;all&lt;/i&gt; your stats data (from November 26th 2007 onwards) for the currently auth'd user.
		/// &lt;b&gt;Please note, these files will only be available until June 1, 2010 Noon PDT.&lt;/b&gt;
		/// For more information &lt;a href="/help/stats/#1369409"&gt;please check out this FAQ&lt;/a&gt;, or just &lt;a href="/photos/me/stats/downloads/"&gt;go download your files&lt;/a&gt;.
		/// </summary>
		public async Task<CsvFileCollection> StatsGetCSVFilesAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getCSVFiles");
			return await GetResponseAsync<CsvFileCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotoDomains
		/// <summary>Get a list of referring domains for a photo</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="photoId">The id of the photo to get stats for. If not provided, stats for all photos will be returned.</param>
		/// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<StatDomainCollection> StatsGetPhotoDomainsAsync(DateTime date, string photoId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotoDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (photoId != null) dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatDomainCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotoReferrers
		/// <summary>Get a list of referrers from a given domain to a photo</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
		/// <param name="photoId">The id of the photo to get stats for. If not provided, stats for all photos will be returned.</param>
		/// <param name="perPage">Number of referrers to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<StatReferrerCollection> StatsGetPhotoReferrersAsync(DateTime date, string domain, string photoId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotoReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (photoId != null) dictionary.Add("photo_id", photoId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatReferrerCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotosetDomains
		/// <summary>Get a list of referring domains for a photoset</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="photosetId">The id of the photoset to get stats for. If not provided, stats for all sets will be returned.</param>
		/// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<StatDomainCollection> StatsGetPhotosetDomainsAsync(DateTime date, string photosetId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotosetDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (photosetId != null) dictionary.Add("photoset_id", photosetId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatDomainCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotosetReferrers
		/// <summary>Get a list of referrers from a given domain to a photoset</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
		/// <param name="photosetId">The id of the photoset to get stats for. If not provided, stats for all sets will be returned.</param>
		/// <param name="perPage">Number of referrers to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<StatReferrerCollection> StatsGetPhotosetReferrersAsync(DateTime date, string domain, string photosetId = null, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotosetReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (photosetId != null) dictionary.Add("photoset_id", photosetId);
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatReferrerCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotosetStats
		/// <summary>Get the number of views on a photoset for a given date.</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="photosetId">The id of the photoset to get stats for.</param>
		public async Task<Stats> StatsGetPhotosetStatsAsync(DateTime date, string photosetId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotosetStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("photoset_id", photosetId);
			return await GetResponseAsync<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotoStats
		/// <summary>Get the number of views, comments and favorites on a photo for a given date.</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="photoId">The id of the photo to get stats for.</param>
		public async Task<Stats> StatsGetPhotoStatsAsync(DateTime date, string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotoStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotostreamDomains
		/// <summary>Get a list of referring domains for a photostream</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<StatDomainCollection> StatsGetPhotostreamDomainsAsync(DateTime date, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotostreamDomains");
			dictionary.Add("date", date.ToUnixTimestamp());
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatDomainCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotostreamReferrers
		/// <summary>Get a list of referrers from a given domain to a user's photostream</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		/// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
		/// <param name="perPage">Number of referrers to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<StatReferrerCollection> StatsGetPhotostreamReferrersAsync(DateTime date, string domain, int perPage = 0, int page = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotostreamReferrers");
			dictionary.Add("date", date.ToUnixTimestamp());
			dictionary.Add("domain", domain);
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<StatReferrerCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getPhotostreamStats
		/// <summary>Get the number of views on a user's photostream for a given date.</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// </param>
		public async Task<Stats> StatsGetPhotostreamStatsAsync(DateTime date) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPhotostreamStats");
			dictionary.Add("date", date.ToUnixTimestamp());
			return await GetResponseAsync<Stats>(dictionary);
		}
		#endregion

		#region flickr.stats.getPopularPhotos
		/// <summary>List the photos with the most views, comments or favorites</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// If no date is provided, all time view counts will be returned.
		/// </param>
		/// <param name="sort">
		/// The order in which to sort returned photos. Defaults to views. The possible values are views, comments and favorites.
		/// Other sort options are available through &lt;a href="/services/api/flickr.photos.search.html"&gt;flickr.photos.search&lt;/a&gt;.
		/// </param>
		/// <param name="perPage">Number of referrers to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		public async Task<PopularPhotoCollection> StatsGetPopularPhotosAsync(DateTime? date = null, PopularitySort sort = PopularitySort.None, int page = 0, int perPage = 0) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getPopularPhotos");
			if (date != null) dictionary.Add("date", date.Value.ToUnixTimestamp());
			if (sort != PopularitySort.None) dictionary.Add("sort", sort.ToString().ToLower());
			if (page != 0) dictionary.Add("page", page.ToString(CultureInfo.InvariantCulture));
			if (perPage != 0) dictionary.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
			return await GetResponseAsync<PopularPhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.stats.getTotalViews
		/// <summary>Get the overall view counts for an account</summary>
		/// <param name="date">
		/// Stats will be returned for this date. This should be in either be in YYYY-MM-DD or unix timestamp format.
		/// A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.
		/// If no date is provided, all time view counts will be returned.
		/// </param>
		public async Task<StatViews> StatsGetTotalViewsAsync(DateTime? date = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.stats.getTotalViews");
			if (date != null) dictionary.Add("date", date.Value.ToUnixTimestamp());
			return await GetResponseAsync<StatViews>(dictionary);
		}
		#endregion

		#region flickr.tags.getClusterPhotos
		/// <summary>Returns the first 24 photos for a given tag cluster</summary>
		/// <param name="tag">The tag that this cluster belongs to.</param>
		/// <param name="clusterId">The top three tags for the cluster, separated by dashes (just like the url).</param>
		public async Task<PhotoCollection> TagsGetClusterPhotosAsync(string tag, string clusterId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getClusterPhotos");
			dictionary.Add("tag", tag);
			dictionary.Add("cluster_id", clusterId);
			return await GetResponseAsync<PhotoCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getClusters
		/// <summary>Gives you a list of tag clusters for the given tag.</summary>
		/// <param name="tag">The tag to fetch clusters for.</param>
		public async Task<ClusterCollection> TagsGetClustersAsync(string tag) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getClusters");
			dictionary.Add("tag", tag);
			return await GetResponseAsync<ClusterCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getHotList
		/// <summary>Returns a list of hot tags for the given period.</summary>
		/// <param name="period">The period for which to fetch hot tags. Valid values are &lt;code&gt;day&lt;/code&gt; and &lt;code&gt;week&lt;/code&gt; (defaults to &lt;code&gt;day&lt;/code&gt;).</param>
		/// <param name="count">The number of tags to return. Defaults to 20. Maximum allowed value is 200.</param>
		public async Task<HotTagCollection> TagsGetHotListAsync(string period = null, int? count = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getHotList");
			if (period != null) dictionary.Add("period", period);
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			return await GetResponseAsync<HotTagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getListPhoto
		/// <summary>Get the tag list for a given photo.</summary>
		/// <param name="photoId">The id of the photo to return tags for.</param>
		public async Task<PhotoInfoTagCollection> TagsGetListPhotoAsync(string photoId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListPhoto");
			dictionary.Add("photo_id", photoId);
			return await GetResponseAsync<PhotoInfoTagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getListUser
		/// <summary>Get the tag list for a given user (or the currently logged in user).</summary>
		/// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
		public async Task<TagCollection> TagsGetListUserAsync(string userId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListUser");
			if (userId != null) dictionary.Add("user_id", userId);
			return await GetResponseAsync<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getListUserPopular
		/// <summary>Get the popular tags for a given user (or the currently logged in user).</summary>
		/// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
		/// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
		public async Task<TagCollection> TagsGetListUserPopularAsync(string userId = null, int? count = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListUserPopular");
			if (userId != null) dictionary.Add("user_id", userId);
			if (count != null) dictionary.Add("count", count.ToString().ToLower());
			return await GetResponseAsync<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getListUserRaw
		/// <summary>Get the raw versions of a given tag (or all tags) for the currently logged-in user.</summary>
		/// <param name="tag">The tag you want to retrieve all raw versions for.</param>
		public async Task<RawTagCollection> TagsGetListUserRawAsync(string tag = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getListUserRaw");
			if (tag != null) dictionary.Add("tag", tag);
			return await GetResponseAsync<RawTagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getMostFrequentlyUsed
		/// <summary>Returns a list of most frequently used tags for a user.</summary>
		public async Task<TagCollection> TagsGetMostFrequentlyUsedAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getMostFrequentlyUsed");
			return await GetResponseAsync<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.tags.getRelated
		/// <summary>Returns a list of tags 'related' to the given tag, based on clustered usage analysis.</summary>
		/// <param name="tag">The tag to fetch related tags for.</param>
		public async Task<TagCollection> TagsGetRelatedAsync(string tag) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.tags.getRelated");
			dictionary.Add("tag", tag);
			return await GetResponseAsync<TagCollection>(dictionary);
		}
		#endregion

		#region flickr.test.echo
		/// <summary>A testing method which echo's all parameters back in the response.</summary>
		public async Task<EchoResponseDictionary> TestEchoAsync(Dictionary<string,string> parameters) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.test.echo");
			dictionary.Add("parameters", parameters.ToString().ToLower());
			return await GetResponseAsync<EchoResponseDictionary>(dictionary);
		}
		#endregion

		#region flickr.test.login
		/// <summary>A testing method which checks if the caller is logged in then returns their username.</summary>
		public async Task<FoundUser> TestLoginAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.test.login");
			return await GetResponseAsync<FoundUser>(dictionary);
		}
		#endregion

		#region flickr.test.null
		/// <summary>Null test</summary>
		public async Task TestNullAsync() 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.test.null");
			await GetResponseAsync(dictionary);
		}
		#endregion

		#region flickr.urls.getGroup
		/// <summary>Returns the url to a group's page.</summary>
		/// <param name="groupId">The NSID of the group to fetch the url for.</param>
		public async Task<string> UrlsGetGroupAsync(string groupId) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.getGroup");
			dictionary.Add("group_id", groupId);
			var result = await GetResponseAsync<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.getUserPhotos
		/// <summary>Returns the url to a user's photos.</summary>
		/// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
		public async Task<string> UrlsGetUserPhotosAsync(string userId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.getUserPhotos");
			if (userId != null) dictionary.Add("user_id", userId);
			var result = await GetResponseAsync<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.getUserProfile
		/// <summary>Returns the url to a user's profile.</summary>
		/// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
		public async Task<string> UrlsGetUserProfileAsync(string userId = null) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.getUserProfile");
			if (userId != null) dictionary.Add("user_id", userId);
			var result = await GetResponseAsync<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.lookupGallery
		/// <summary>Returns gallery info, by url.</summary>
		/// <param name="url">The gallery's URL.</param>
		public async Task<Gallery> UrlsLookupGalleryAsync(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.lookupGallery");
			dictionary.Add("url", url);
			return await GetResponseAsync<Gallery>(dictionary);
		}
		#endregion

		#region flickr.urls.lookupGroup
		/// <summary>Returns a group NSID, given the url to a group's page or photo pool.</summary>
		/// <param name="url">The url to the group's page or photo pool.</param>
		public async Task<string> UrlsLookupGroupAsync(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.lookupGroup");
			dictionary.Add("url", url);
			var result = await GetResponseAsync<StringHolder>(dictionary);
			return result.Value;
		}
		#endregion

		#region flickr.urls.lookupUser
		/// <summary>Returns a user NSID, given the url to a user's photos or profile.</summary>
		/// <param name="url">The url to the user's profile or photos page.</param>
		public async Task<FoundUser> UrlsLookupUserAsync(string url) 
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("method", "flickr.urls.lookupUser");
			dictionary.Add("url", url);
			return await GetResponseAsync<FoundUser>(dictionary);
		}
		#endregion
	}

}
