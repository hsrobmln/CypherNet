﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CypherNet.Http
{
    public class WebClient : IWebClient
    {
        #region IWebClient Members

        public async Task<IHttpResponseMessage> GetAsync(string url)
        {
            return await Execute(url, HttpMethod.Get);
        }

        public async Task<IHttpResponseMessage> PostAsync(string url, String body)
        {
            return await Execute(url, body, HttpMethod.Post);
        }

        public async Task<IHttpResponseMessage> PutAsync(string url, String body)
        {
            return await Execute(url, body, HttpMethod.Put);
        }

        public async Task<IHttpResponseMessage> DeleteAsync(string url)
        {
            return await Execute(url, HttpMethod.Delete);
        }

        #endregion

        private async Task<IHttpResponseMessage> Execute(string url, HttpMethod method)
        {
            var msg = new HttpRequestMessage(method, url);

            using (var client = new HttpClient())
            {
                var result = await client.SendAsync(msg);
                return new HttpResponseMessageWrapper(result);
            }
        }

        private async Task<IHttpResponseMessage> Execute(string url, String content, HttpMethod method)
        {
            var msg = new HttpRequestMessage(method, url);

            if (content != null)
            {
                msg.Content = new StringContent(content);
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            using (var client = new HttpClient())
            {
                var result = await client.SendAsync(msg);
                return new HttpResponseMessageWrapper(result);
            }
        }
    }

}