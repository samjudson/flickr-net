using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Add a person to a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to add a person to.</param>
        /// <param name="userId">The NSID of the user to add to the photo.</param>
        public void PhotosPeopleAdd(string photoId, string userId)
        {
            PhotosPeopleAdd(photoId, userId, null, null, null, null);
        }

        /// <summary>
        /// Add a person to a photo. Coordinates and sizes of boxes are optional; they are measured in pixels, based on the 500px image size shown on individual photo pages.
        /// </summary>
        /// <param name="photoId">The id of the photo to add a person to.</param>
        /// <param name="userId">The NSID of the user to add to the photo.</param>
        /// <param name="personX">The left-most pixel co-ordinate of the box around the person.</param>
        /// <param name="personY">The top-most pixel co-ordinate of the box around the person.</param>
        /// <param name="personWidth">The width (in pixels) of the box around the person.</param>
        /// <param name="personHeight">The height (in pixels) of the box around the person.</param>
        public void PhotosPeopleAdd(string photoId, string userId, int? personX, int? personY, int? personWidth, int? personHeight)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.add");
            parameters.Add("photo_id", photoId);
            parameters.Add("user_id", userId);
            if (personX != null) parameters.Add("person_x", personX.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (personY != null) parameters.Add("person_y", personY.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (personWidth != null) parameters.Add("person_w", personWidth.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (personHeight != null) parameters.Add("person_h", personHeight.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Remove a person from a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to remove a person from.</param>
        /// <param name="userId">The NSID of the person to remove from the photo.</param>
        public void PhotosPeopleDelete(string photoId, string userId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.delete");
            parameters.Add("photo_id", photoId);
            parameters.Add("user_id", userId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Remove the bounding box from a person in a photo
        /// </summary>
        /// <param name="photoId">The id of the photo to edit a person in.</param>
        /// <param name="userId">The NSID of the person whose bounding box you want to remove.</param>
        public void PhotosPeopleDeleteCoords(string photoId, string userId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.deleteCoords");
            parameters.Add("photo_id", photoId);
            parameters.Add("user_id", userId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Edit the bounding box of an existing person on a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to edit a person in.</param>
        /// <param name="userId">The NSID of the person to edit in a photo.</param>
        /// <param name="personX">The left-most pixel co-ordinate of the box around the person.</param>
        /// <param name="personY">The top-most pixel co-ordinate of the box around the person.</param>
        /// <param name="personWidth">The width (in pixels) of the box around the person.</param>
        /// <param name="personHeight">The height (in pixels) of the box around the person.</param>
        public void PhotosPeopleEditCoords(string photoId, string userId, int personX, int personY, int personWidth, int personHeight)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.editCoords");
            parameters.Add("photo_id", photoId);
            parameters.Add("user_id", userId);
            parameters.Add("person_x", personX.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("person_y", personY.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("person_w", personWidth.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("person_h", personHeight.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Get a list of people in a given photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to get a list of people for.</param>
        /// <returns>A collection of <see cref="PhotoPerson"/> instances.</returns>
        public PhotoPersonCollection PhotosPeopleGetList(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.getList");
            parameters.Add("photo_id", photoId);

            return GetResponseNoCache<PhotoPersonCollection>(parameters);
        }


    }
}
