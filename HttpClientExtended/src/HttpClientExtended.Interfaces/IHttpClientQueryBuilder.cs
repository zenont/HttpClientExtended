using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientExtended.Interfaces
{
    public interface IHttpClientQueryBuilder
    {
        Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IHttpClientQueryBuilder<T> : IHttpClientQueryBuilder where T : HttpClient
    {
        IHttpClientQueryBuilder<T> Query(string key, string value);
    }
}
