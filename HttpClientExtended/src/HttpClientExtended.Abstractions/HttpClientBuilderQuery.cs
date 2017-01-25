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
    public class HttpClientBuilderQuery<T> : IHttpClientQueryBuilder<T> where T : HttpClient
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

        protected IDictionary<string, IEnumerable<string>> Headers = new Dictionary<string, IEnumerable<string>>();

        public IHttpClientQueryBuilder<T> Query(string key, params object[] value)
        {
            QueryString.Add(key, value);
            return this;
        }

        public IHttpClientQueryBuilder<T> Header(string key, params string[] value)
        {
            Headers.Add(key, value);
            return this;
        }

        public Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod, RequestUri))
            {
                foreach(var header in Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                if (Content != null)
                {
                    request.Content = Content;
                }

                return HttpClient.SendAsync(request, cancellationToken);
            }
        }
    }
}
