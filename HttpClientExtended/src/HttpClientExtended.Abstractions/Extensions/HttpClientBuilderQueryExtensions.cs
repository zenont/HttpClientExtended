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
    public static class HttpClientBuilderQueryExtensions
    {
        public static IHttpClientQueryBuilder<KClient> Query<KClient, TValue>(this IHttpClientQueryBuilder<KClient> builder, string key, params TValue[] value) where KClient:HttpClient
        {
            foreach (TValue arrayValue in value)
            {
                if (arrayValue == null) continue;

                if (arrayValue is DateTime)
                {
                    builder.Query(key, Convert.ToDateTime(arrayValue));
                }
                else
                {
                    builder.Query(key, Convert.ToString(arrayValue));
                }
            }
            return builder;
        }

        public static IHttpClientQueryBuilder<KClient> Query<KClient>(this IHttpClientQueryBuilder<KClient> builder, string key, DateTime? value) where KClient : HttpClient
        {
            if (!value.HasValue) return builder;
            builder.Query(key, value.Value.ToString("o"));
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
