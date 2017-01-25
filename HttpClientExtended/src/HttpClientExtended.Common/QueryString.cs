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

        protected virtual bool TryParseFromDate(object value, out string convertedValue)
        {
            convertedValue = null;
            
            if (value is DateTime)
            {
                DateTime dateTime = (DateTime)value;
                convertedValue = dateTime.ToString("o");
                return true;
            }
            return false;
        }

        protected virtual string ConvertValueToString(object value)
        {
            if (value == null) return null;

            string convertedValue;
            if(TryParseFromDate(value, out convertedValue))
            {
                return convertedValue;
            }

            return Convert.ToString(value)?.Trim();
        }

        public virtual void Add(string key, params object[] value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null || !value.Any())
            {
                return;
            }

            foreach (var v in value)
            {
                ICollection collection = v as ICollection;

                if(collection != null)
                {
                    if (collection.Count <= 0) continue;

                    // if we are enumerable, then we recurse and then skip to the next one
                    Add(key, collection.Cast<object>().ToArray());
                    continue;
                }

                string convertedValue = ConvertValueToString(v);
                if (string.IsNullOrWhiteSpace(convertedValue))
                {
                    continue;
                }

                Add(new KeyValuePair<string, string>(key, convertedValue));
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
