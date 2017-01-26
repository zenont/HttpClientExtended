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
    public class HttpClientQueryBuilder<T> : IHttpClientQueryBuilder<T> where T : HttpClient
    {
        public HttpClientQueryBuilder(T httpClient, HttpMethod httpMethod, string requestUri, HttpContent content = null)
        {
            HttpClient = httpClient;
            HttpMethod = httpMethod;
            Content = content;
            RequestUri = requestUri;
        }

        public T HttpClient { get; protected set; }

        public HttpMethod HttpMethod { get; protected set; }

        public HttpContent Content { get; protected set; }

        public string RequestUri { get; protected set; }

        public QueryString QueryString { get; protected set; } = new QueryString();

        public IDictionary<string, IEnumerable<string>> Headers { get; protected set; } = new Dictionary<string, IEnumerable<string>>();

        HttpClient IHttpClientQueryBuilder.HttpClient
        {
            get
            {
                return HttpClient;
            }
        }

        public IHttpClientQueryBuilder<T> Query(string key, object value)
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
