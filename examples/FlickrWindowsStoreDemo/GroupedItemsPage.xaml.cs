using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FlickrNet;
using FlickrWindowsStoreDemo.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace FlickrWindowsStoreDemo
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : FlickrWindowsStoreDemo.Common.LayoutAwarePage
    {
        public GroupedItemsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroups = SampleDataSource.GetGroups((String)navigationParameter);
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetailPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }

        private OAuthAccessToken AccessToken
        {
            get { return (Application.Current as App).AccessToken; }
            set { (Application.Current as App).AccessToken = value; }
        }

        private async Task<OAuthRequestToken> GetRequestToken()
        {
            var f = new Flickr("dbc316af64fb77dae9140de64262da0a", "0781969a058a2745");
            return await f.OAuthRequestTokenAsync(WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString());
        }

        private async void PageRootLoaded(object sender, RoutedEventArgs e)
        {
            if (AccessToken != null) return;

            var f = new Flickr("dbc316af64fb77dae9140de64262da0a", "0781969a058a2745");
            var requestToken = await GetRequestToken();
            string output;
            var flickrUri = new Uri(f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete));
            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                    WebAuthenticationOptions.None,
                                                    flickrUri);

            if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                output = webAuthenticationResult.ResponseData;
                AccessToken = await f.OAuthAccessTokenAsync(requestToken.Token, requestToken.TokenSecret, output);

                LoadDataSource(AccessToken);
            }
            else if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                output = "HTTP Error returned by AuthenticateAsync() : " + webAuthenticationResult.ResponseErrorDetail.ToString();
            }
            else if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                output = "Authentication process was cancelled by the user";
            } 
        }

        private async void LoadDataSource(OAuthAccessToken accessToken)
        {
            var f = new Flickr("dbc316af64fb77dae9140de64262da0a", "0781969a058a2745")
                        {
                            OAuthAccessToken = accessToken.Token,
                            OAuthAccessTokenSecret = accessToken.TokenSecret
                        };

            var groups = DefaultViewModel["Groups"] as ObservableCollection<SampleDataGroup>;
            groups.Clear();
            var photostreamGroup = new SampleDataGroup("A", "Your Photos", "Photostream", "", "");
            groups.Add(photostreamGroup);

            var contactsGroup = new SampleDataGroup("A", "Your Contact", "Latest Updates", "", "");
            groups.Add(contactsGroup);

            var favouritesGroup = new SampleDataGroup("A", "Your Favourites", "Favourite Photos", "", "");
            groups.Add(favouritesGroup);

            var photos = await f.PeopleGetPhotosAsync(accessToken.UserId, SafetyLevel.None, null, null, null, null,
                                                      ContentTypeSearch.None, PrivacyFilter.None,
                                                      PhotoSearchExtras.Description | PhotoSearchExtras.LargeUrl, 1,
                                                      30);
            foreach (
                var photo in
                    photos.Select(
                        p => new SampleDataItem(p.PhotoId, p.Title, "", p.LargeUrl, p.Description, p.Description, photostreamGroup))
                )
            {
                photostreamGroup.Items.Add(photo);
            }

            photos =
                await f.PhotosGetContactsPhotosAsync(extras: PhotoSearchExtras.Description | PhotoSearchExtras.LargeUrl | PhotoSearchExtras.OwnerName);
            foreach (
                var photo in
                    photos.Select(
                        p => new SampleDataItem(p.PhotoId, p.Title, p.OwnerName, p.LargeUrl, p.Description, p.Description, contactsGroup))
                )
            {
                contactsGroup.Items.Add(photo);
            }

            photos = await f.FavoritesGetListAsync(extras: PhotoSearchExtras.Description | PhotoSearchExtras.LargeUrl | PhotoSearchExtras.OwnerName);
            foreach (
                var photo in
                    photos.Select(
                        p => new SampleDataItem(p.PhotoId, p.Title, p.OwnerName, p.LargeUrl, p.Description, p.Description, favouritesGroup))
                )
            {
                favouritesGroup.Items.Add(photo);
            }
        }
    }
}
