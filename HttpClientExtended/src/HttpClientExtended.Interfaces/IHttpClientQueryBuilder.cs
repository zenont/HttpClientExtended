using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExtended.Interfaces
{
    public interface IHttpClientQueryBuilder<T> where T : HttpClient
    {
        IHttpClientQueryBuilder<T> Query(string key, string value);
    }
}
