using HttpClientExtended.Common;
using HttpClientExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientExtended.Abstractions
{
    public class HttpClientBuilderQuery<T> : IHttpClientQueryBuilder<T> where T: HttpClient
    {
        public HttpClientBuilderQuery(T httpClient, HttpMethod httpMethod, string requestUri, HttpContent content = null)
        {
            HttpClient = httpClient;
            HttpMethod = httpMethod;
            Content = content;
            RequestUri = requestUri;
        }

        protected T HttpClient { get; set; }

        protected HttpMethod HttpMethod { get; set; }

        protected HttpContent Content { get; set; }

        protected string RequestUri { get; set; }

        protected QueryString QueryString { get; set; } = new QueryString();

        public IHttpClientQueryBuilder<T> Query(string key, string value)
        {
            QueryString.Add(key, value);
            return this;
        }

        public Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod, RequestUri))
            {
                if (Content != null)
                {
                    requestMessage.Content = Content;
                }

                return HttpClient.SendAsync(requestMessage, cancellationToken);
            }
        }
    }
}
