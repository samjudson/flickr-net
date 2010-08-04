using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets a collection of Flickr Commons institutions.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void CommonsGetInstitutionsAsync(Action<FlickrResult<InstitutionCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.commons.getInstitutions");

            GetResponseAsync<InstitutionCollection>(parameters, callback);
        }
    }
}
