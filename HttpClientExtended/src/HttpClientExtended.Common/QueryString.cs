using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExtended.Common
{
    public class QueryString:List<KeyValuePair<string, string>>
    {
        public QueryString()
        {
        }

        protected virtual string ConvertValueToString(object value)
        {
            return Convert.ToString(value);
        }

        public virtual void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key))  throw new ArgumentNullException("key");
            if (string.IsNullOrEmpty(value)) return;

            Add(new KeyValuePair<string, string>(key.Trim(), value.Trim()));
        }

        public virtual void Add<T>(string key, IEnumerable<T> value)
        {
            foreach (T arrayValue in value)
            {
                if(arrayValue is DateTime)
                {
                    Add(key, Convert.ToDateTime(arrayValue));
                }
                else
                {
                    Add(key, ConvertValueToString(arrayValue));
                }
            }
        }

        public virtual void Add(string key, DateTime? value)
        {
            if (!value.HasValue) return;
            Add(key, value.Value.ToString("o"));
        }

        public virtual async Task<Uri> AsUriAsync(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentNullException("baseUrl");

            UriKind uriKind = UriKind.RelativeOrAbsolute;

            Uri uri = !this.Any()
                ? new Uri(baseUrl, uriKind)
                : new Uri($"{baseUrl}?{await AsUrlAsync()}",
                    uriKind);

            return uri;
        }

        public virtual async Task<string> AsUrlAsync()
        {
            string query;
            using (var content = new FormUrlEncodedContent(this))
            {
                query = await content.ReadAsStringAsync();
            }

            return query;
        }
    }
}
