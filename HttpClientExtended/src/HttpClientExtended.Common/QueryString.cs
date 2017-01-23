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

        public virtual void AddKeyValuePair(string key, string value)
        {
            if (string.IsNullOrEmpty(key))  throw new ArgumentNullException("key");
            if (string.IsNullOrEmpty(value)) return;

            Add(new KeyValuePair<string, string>(key.Trim(), value.Trim()));
        }

        public virtual void AddQueryString(string key, object value)
        {
            // we check if the value is array, if so, then we have to break the array into multiple key value pairs
            bool isEnumerable = value?.GetType().IsArray ?? false;
            if (isEnumerable)
            {
                IEnumerable array = (IEnumerable)value;
                foreach(object arrayValue in array)
                {
                    AddKeyValuePair(key, ConvertValueToString(arrayValue));
                }
            }
            else
            {
                AddKeyValuePair(key, ConvertValueToString(value));
            }
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
