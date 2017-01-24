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
        public HttpClientBuilderQuery(T httpClient, HttpMethod httpMethod, HttpContent content)
        {
            HttpClient = httpClient;
            HttpMethod = httpMethod;
            Content = content;
        }

        protected T HttpClient { get; set; }

        protected HttpMethod HttpMethod { get; set; }

        protected HttpContent Content { get; set; }

        protected QueryString QueryString { get; set; } = new QueryString();
        
        public virtual IHttpClientQueryBuilder<T> Query(string key, string value)
        {
            QueryString.Add(key, value);
            return this;
        }

        public virtual Task AsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
     
            HttpClient.SendAsync()
        }
    }
}
