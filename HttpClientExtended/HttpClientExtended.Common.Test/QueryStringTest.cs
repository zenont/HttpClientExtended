using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientExtended.Common.Test
{
    public class QueryStringTest
    {
        [Theory]
        [InlineData("someKey", true, "True")]
        [InlineData("someKey", 10, "10")]
        [InlineData("someKey", 10.103, "10.103")]
        [InlineData("someKey", null, "")]
        public void ShouldAddAndParseMultipleDataTypes(string key, object value, string expected)
        {
            // act
            QueryString queryString = new QueryString();
            queryString.AddQueryString(key, value);

            // assert
            Assert.True(queryString.Single().Value == expected);
        }

        [Theory]
        [InlineData("someKey", new int[] { 1, 2, 3, 4 }, new string[] { "1", "2", "3", "4" })]
        public void ShouldAddAndParseArrays(string key, object value, string[] expected)
        {
            // act
            QueryString queryString = new QueryString();
            queryString.AddQueryString(key, value);
            KeyValuePair<string, string>[] parsedValue = queryString.Where(x => x.Key == key).ToArray();


            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(parsedValue[i].Value == expected[i]);
            }
        }

        [Fact]
        public void ShouldAddAndParseIEnumerableFromList()
        {
            // act
            const string key = "someKey";
            List<int> value = new List<int> { 1, 2, 3, 4 };
            var expected = new string[] { "1", "2", "3", "4" };

            // act
            QueryString queryString = new QueryString();
            queryString.AddQueryString(key, value);
            KeyValuePair<string, string>[] parsedValue = queryString.Where(x => x.Key == key).ToArray();

            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(parsedValue[i].Value == expected[i]);
            }
        }
    }
}
