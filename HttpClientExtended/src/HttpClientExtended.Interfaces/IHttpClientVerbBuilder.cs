using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExtended.Interfaces
{
    public interface IHttpClientVerbBuilder<T> where T:HttpClient
    {
        IHttpClientQueryBuilder<T> Get(Uri uri);

        IHttpClientQueryBuilder<T> Post(Uri uri);

        IHttpClientQueryBuilder<T> Post(Uri uri, HttpContent content);

        IHttpClientQueryBuilder<T> Put(Uri uri);

        IHttpClientQueryBuilder<T> Put(Uri uri, HttpContent content);

        IHttpClientQueryBuilder<T> Delete(Uri uri);

        IHttpClientQueryBuilder<T> Head(Uri uri);
    }
}
