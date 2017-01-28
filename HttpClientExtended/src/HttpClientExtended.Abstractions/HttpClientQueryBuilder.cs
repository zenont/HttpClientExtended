using HttpClientExtended.Common;
using HttpClientExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public IDictionary<string, string[]> Headers { get; protected set; } = new Dictionary<string, string[]>();

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

        public virtual async Task<HttpRequestMessage> BuildHttpRequestAsync()
        {
            if (string.IsNullOrWhiteSpace(RequestUri))
                throw new ArgumentNullException(nameof(RequestUri));

            if (HttpMethod == null)
                throw new ArgumentNullException(nameof(HttpMethod));

            if (!Uri.IsWellFormedUriString(RequestUri, UriKind.RelativeOrAbsolute))
                throw new UriFormatException(RequestUri);

            Uri uri = await QueryString.AsUriAsync(RequestUri);

            var request = new HttpRequestMessage(HttpMethod, uri.ToString());

            foreach (var header in Headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (Content != null)
            {
                request.Content = Content;
            }

            return request;
        }

        public async Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpRequestMessage request = await BuildHttpRequestAsync())
            {
                return await HttpClient.SendAsync(request, cancellationToken);
            }
        }
    }
}
