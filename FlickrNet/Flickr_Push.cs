using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get a list of subscriptions for the calling user.
        /// </summary>
        public SubscriptionCollection PushGetSubscriptions()
        {
            CheckRequiresAuthentication();

            var parameters = new Dictionary<string, string>() { { "method", "flickr.push.getSubscriptions" } };

            return GetResponseCache<SubscriptionCollection>(parameters);
        }

        /// <summary>
        /// Get a list of topics that are available for subscription.
        /// </summary>
        public string[] PushGetTopics()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.push.getTopics");

            var response = GetResponseCache<UnknownResponse>(parameters);
            
            List<string> topics = new List<string>();
            foreach (System.Xml.XmlNode node in response.GetXmlDocument().SelectNodes("//topic/@name"))
            {
                topics.Add(node.Value);
            }

            return topics.ToArray();
        }

        /// <summary>
        /// Subscribe to a particular topic.
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        /// <param name="callback">The callback url.</param>
        /// <param name="verify">Either 'sync' or 'async'.</param>
        /// <param name="verifyToken">An optional token to be sent along with the verification.</param>
        /// <param name="leaseSeconds">The number of seconds the lease should be for.</param>
        /// <param name="woeIds">An array of WOE ids to listen to. Only applies if topic is 'geo'.</param>
        /// <param name="placeIds">An array of place ids to subscribe to. Only applies if topic is 'geo'.</param>
        /// <param name="latitude">The latitude to subscribe to. Only applies if topic is 'geo'.</param>
        /// <param name="longitude">The longitude to subscribe to. Only applies if topic is 'geo'.</param>
        /// <param name="radius">The radius to subscribe to. Only applies if topic is 'geo'.</param>
        /// <param name="radiusUnits">The raduis units to subscribe to. Only applies if topic is 'geo'.</param>
        /// <param name="accuracy">The accuracy of the geo search to subscribe to. Only applies if topic is 'geo'.</param>
        /// <param name="nsids">A list of Commons Institutes to subscribe to. Only applies if topic is 'commons'. If not present this argument defaults to all Flickr Commons institutions.</param>
        /// <param name="tags">A list of strings to be used for tag subscriptions. Photos with one or more of the tags listed will be included in the subscription. Only valid if the topic is 'tags'</param>
        public void PushSubscribe(string topic, string callback, string verify, string verifyToken, int leaseSeconds, int[] woeIds, string[] placeIds, double latitude, double longitude, int radius, RadiusUnit radiusUnits, GeoAccuracy accuracy, string[] nsids, string[] tags)
        {
            CheckRequiresAuthentication();

            if (String.IsNullOrEmpty(topic)) throw new ArgumentNullException("topic");
            if (String.IsNullOrEmpty(callback)) throw new ArgumentNullException("callback");
            if (String.IsNullOrEmpty(verify)) throw new ArgumentNullException("verify");

            if (topic == "tags" && (tags == null || tags.Length == 0)) throw new InvalidOperationException("Must specify at least one tag is using topic of 'tags'");

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.push.subscribe");
            parameters.Add("topic", topic);
            parameters.Add("callback", callback);
            parameters.Add("verify", verify);
            if (!String.IsNullOrEmpty(verifyToken)) parameters.Add("verify_token", verifyToken);
            if (leaseSeconds > 0) parameters.Add("lease_seconds", leaseSeconds.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (woeIds != null && woeIds.Length > 0)
            {
                string[] woeIdsString = Array.ConvertAll<int, string>(woeIds, i => i.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                parameters.Add("woe_ids", String.Join(",", woeIdsString));
            }
            if (placeIds != null && placeIds.Length > 0) parameters.Add("place_ids", String.Join(",", placeIds));
            if (radiusUnits != RadiusUnit.None) parameters.Add("radius_units", radiusUnits.ToString("d"));

            GetResponseNoCache<NoResponse>(parameters);

        }

        /// <summary>
        /// Unsubscribe from a particular push subscription.
        /// </summary>
        /// <param name="topic">The topic to unsubscribe from.</param>
        /// <param name="callback">The callback url to unsubscribe.</param>
        /// <param name="verify">Either 'sync' or 'async'.</param>
        /// <param name="verifyToken">The verification token to include in the unsubscribe verification process.</param>
        public void PushUnsubscribe(string topic, string callback, string verify, string verifyToken)
        {
            CheckRequiresAuthentication();

            if (String.IsNullOrEmpty(topic)) throw new ArgumentNullException("topic");
            if (String.IsNullOrEmpty(callback)) throw new ArgumentNullException("callback");
            if (String.IsNullOrEmpty(verify)) throw new ArgumentNullException("verify");

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.push.unsubscribe");
            parameters.Add("topic", topic);
            parameters.Add("callback", callback);
            parameters.Add("verify", verify);
            if (!String.IsNullOrEmpty(verifyToken)) parameters.Add("verif_token", verifyToken);

            GetResponseNoCache<NoResponse>(parameters);
        }
    }
}
