using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientExtended.Abstractions.Extensions
{
    public static class HttpHeaderExtensions
    {
        public static void Add(this HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> header, string mediaType)
        {
            header.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        public static void Add(this HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> header, string mediaType, int quality)
        {
            header.Add(new MediaTypeWithQualityHeaderValue(mediaType, quality));
        }
    }
}
