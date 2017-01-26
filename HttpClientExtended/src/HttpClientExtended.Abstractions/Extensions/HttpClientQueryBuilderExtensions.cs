using HttpClientExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientExtended.Abstractions.Extensions
{
    public static class HttpClientQueryBuilderExtensions
    {
        public static IHttpClientQueryBuilder<KClient> QueryFromArray<KClient, TValue>(this IHttpClientQueryBuilder<KClient> builder, string key, IEnumerable<TValue> value) where KClient:HttpClient
        {
            foreach (TValue arrayValue in value)
            {
                if (arrayValue == null) continue;
                builder.Query(key, arrayValue);                
            }
            return builder;
        }

        public static async Task<T> AsAsync<T>(this IHttpClientQueryBuilder builder, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await builder.SendAsync(cancellationToken);
            return await response.Content.ReadAsAsync<T>();
        }

        public static async Task<Stream> AsStreamAsync<T>(this IHttpClientQueryBuilder builder, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await builder.SendAsync(cancellationToken);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
