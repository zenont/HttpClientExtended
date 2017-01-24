using HttpClientExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClientExtended.Abstractions
{
    public class HttpClientVerbBuilder<T> : IHttpClientVerbBuilder<T> where T : HttpClient
    {
        public HttpClientVerbBuilder(T httpClient)
        {
            HttpClient = httpClient;
        }

        protected T HttpClient { get; set; }

        public IHttpClientQueryBuilder<T> Get(Uri uri)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Get);
        }

        public IHttpClientQueryBuilder<T> Post(Uri uri)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Post);
        }

        public IHttpClientQueryBuilder<T> Post(Uri uri, HttpContent content)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Post);
        }

        public IHttpClientQueryBuilder<T> Put(Uri uri)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Put);
        }

        public IHttpClientQueryBuilder<T> Put(Uri uri, HttpContent content)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Put);
        }

        public IHttpClientQueryBuilder<T> Delete(Uri uri)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Delete);
        }

        public IHttpClientQueryBuilder<T> Head(Uri uri)
        {
            return new HttpClientBuilderQuery<T>(HttpClient, HttpMethod.Head);
        }
    }
}
