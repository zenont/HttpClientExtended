using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        protected virtual string TrimKeyOrThrow(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("The key cannot be empty or blank", "key");
            return key.Trim();
        }

        public void AddKeyValuePair(string key, string value)
        {
            key = TrimKeyOrThrow(key);
            if (string.IsNullOrEmpty(value)) return;

            value = value?.Trim();
            Add(new KeyValuePair<string, string>(key, value));
        }

        public void AddQueryString(string key, object value)
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
    }
}
