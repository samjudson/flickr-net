using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        public void PhotosPeopleAdd(string photoId, string userId)
        {
            PhotosPeopleAdd(photoId, userId, null, null, null, null);
        }

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

        public void PhotosPeopleDelete(string photoId, string userId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.delete");
            parameters.Add("photo_id", photoId);
            parameters.Add("user_id", userId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        public void PhotosPeopleDeleteCoords(string photoId, string userId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.people.deleteCoords");
            parameters.Add("photo_id", photoId);
            parameters.Add("user_id", userId);

            GetResponseNoCache<NoResponse>(parameters);
        }

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

        //public void PhotoPeopleGetList(string photoId)
        //{
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("method", "flickr.photos.people.getList");
        //    parameters.Add("photo_id", photoId);

        //    GetResponseNoCache<NoResponse>(parameters);
        //}


    }
}
