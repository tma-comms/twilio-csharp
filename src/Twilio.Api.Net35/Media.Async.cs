﻿using System;
using Simple;

namespace Twilio
{
    public partial class TwilioRestClient
    {

        /// <summary>
        /// Get the details for a specific Media instance.
        /// </summary>
        public virtual void GetMedia(string mediaSid, Action<Media> callback)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Media/{MediaSid}.json";
            request.AddUrlSegment("MediaSid", mediaSid);
            ExecuteAsync<Media>(request, (response) => callback(response));
        }

        /// <summary>
        /// Retrieve a list of Media objects with no list filters
        /// </summary>
        public virtual void ListMedia(Action<MediaResult> callback)
        {
            ListMedia(new MediaListRequest(), callback);
        }

        /// <summary>
        /// Return a filtered list of Media objects. The list includes paging
        /// information.
        /// </summary>
        public virtual void ListMedia(MediaListRequest options, Action<MediaResult> callback)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Media.json";
            AddMediaListOptions(options, request);
            ExecuteAsync<MediaResult>(request, (response) => callback(response));
        }

        /// <summary>
        /// List all media for a particular message
        /// </summary>
        /// <param name="messageSid">The message sid to filter on</param>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        public virtual void ListMessageMedia(string messageSid, MediaListRequest options, Action<MediaResult> callback)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Messages/{MessageSid}/Media.json";
            request.AddUrlSegment("MessageSid", messageSid);
            AddMediaListOptions(options, request);
            ExecuteAsync<MediaResult>(request, (response) => callback(response));
        }

        /// <summary>
        /// Delete the specified media instance. Makes a DELETE request to a 
        /// Media Instance resource.
        /// </summary>
        /// <param name="mediaSid">The Sid of the media to delete</param>
        /// <param name="callback"></param>
        public virtual void DeleteMedia(string mediaSid, Action<DeleteStatus> callback)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "Accounts/{AccountSid}/Media/{MediaSid}.json";

            request.AddParameter("MediaSid", mediaSid, ParameterType.UrlSegment);

            ExecuteAsync(request, (response) => { callback(response.StatusCode == System.Net.HttpStatusCode.NoContent ? DeleteStatus.Success : DeleteStatus.Failed); });
        }
    }
}