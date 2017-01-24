using HttpClientExtended.Abstractions;
using HttpClientExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace HttpClientExtended.Abstractions.Extensions
{
    public static class HttpClientVerbBuilderExtensions
    {
        public static IHttpClientVerbBuilder<KClient> Request<KClient>(this KClient httpClient) where KClient : HttpClient
        {
            return new HttpClientVerbBuilder<KClient>(httpClient);
        }

        public static IHttpClientQueryBuilder<KClient> PostAsJson<KClient, TContent>(this IHttpClientVerbBuilder<KClient> builder, string requestUri, TContent content) where KClient : HttpClient
        {
            return builder.Post(requestUri, new ObjectContent(typeof(TContent), content, new JsonMediaTypeFormatter()));
        }

        public static IHttpClientQueryBuilder<KClient> PostAsXml<KClient, TContent>(this IHttpClientVerbBuilder<KClient> builder, string requestUri, TContent content) where KClient : HttpClient
        {
            return builder.Post(requestUri, new ObjectContent(typeof(TContent), content, new XmlMediaTypeFormatter()));
        }

        public static IHttpClientQueryBuilder<KClient> PutAsJson<KClient, TContent>(this IHttpClientVerbBuilder<KClient> builder, string requestUri, TContent content) where KClient : HttpClient
        {
            return builder.Put(requestUri, new ObjectContent(typeof(TContent), content, new JsonMediaTypeFormatter()));
        }

        public static IHttpClientQueryBuilder<KClient> PutAsXml<KClient, TContent>(this IHttpClientVerbBuilder<KClient> builder, string requestUri, TContent content) where KClient : HttpClient
        {
            return builder.Put(requestUri, new ObjectContent(typeof(TContent), content, new XmlMediaTypeFormatter()));
        }
    }
}
