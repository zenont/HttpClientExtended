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
        [InlineData("someKey", "True", "True")]
        [InlineData("someKey", "10", "10")]
        [InlineData("someKey", "10.103", "10.103")]
        public void ShouldAddKeyValueStrings(string key, string value, string expected)
        {
            // arrange
            QueryString queryString = new QueryString();

            // act
            queryString.Add(key, value);

            // assert
            Assert.True(queryString.Single().Value == expected);
        }

        [Theory]
        [InlineData("someKey", new int[] { 1, 2, 3, 4 }, new string[] { "1", "2", "3", "4" })]
        public void SHouldAddArray(string key, int[] value, string[] expected)
        {
            // arrange
            QueryString queryString = new QueryString();
            queryString.Add(key, value);
            KeyValuePair<string, string>[] parsedValue = queryString.Where(x => x.Key == key).ToArray();


            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(parsedValue[i].Value == expected[i]);
            }
        }

        [Fact]
        public void ShouldAddListAndIgnoreNullItem()
        {
            // arrange
            const string key = "someKey";
            List<int?> value = new List<int?> { 1, 2, null, 4 };
            var expected = new string[] { "1", "2", "4" };

            // act
            QueryString queryString = new QueryString();
            queryString.Add(key, value);
            KeyValuePair<string, string>[] parsedValue = queryString.Where(x => x.Key == key).ToArray();

            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(parsedValue[i].Value == expected[i]);
            }
        }

        [Fact]
        public void ShouldAddListWithDatetimes()
        {
            // arrange
            const string key = "someKey";
            var value1 = new DateTime(2010, 1, 1, 10, 0, 0);
            var value2 = new DateTime(2010, 2, 1, 10, 0, 0);
            var value3 = new DateTime(2010, 3, 1, 10, 0, 0);

            List<DateTime> value = new List<DateTime> { value1, value2, value3 };
            var expected = new string[] {value1.ToString("o"), value2.ToString("o"), value3.ToString("o") };

            // act
            QueryString queryString = new QueryString();
            queryString.Add(key, value);
            KeyValuePair<string, string>[] parsedValue = queryString.Where(x => x.Key == key).ToArray();

            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(parsedValue[i].Value == expected[i]);
            }
        }

        [Fact]
        public void ShouldAddListWithNullableDatetimes()
        {
            // arrange
            const string key = "someKey";
            var value1 = new DateTime(2010, 1, 1, 10, 0, 0);
            DateTime? value2 = null;
            var value3 = new DateTime(2010, 3, 1, 10, 0, 0);

            List<DateTime?> value = new List<DateTime?> { value1, value2, value3 };
            var expected = new string[] { value1.ToString("o"), value3.ToString("o") };

            // act
            QueryString queryString = new QueryString();
            queryString.Add(key, value);
            KeyValuePair<string, string>[] parsedValue = queryString.Where(x => x.Key == key).ToArray();

            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.True(parsedValue[i].Value == expected[i]);
            }
        }

        [Fact]
        public void ShouldAddDateTime()
        {
            // arrange
            const string key = "key1";
            var value1 = new DateTime(2010, 1, 1, 10, 0, 0);
            QueryString queryString = new QueryString();

            // act
            queryString.Add(key, value1);

            // assert
            Assert.True(queryString.Single().Value == value1.ToString("o"));
        }

        [Fact]
        public void ShouldAddNullableDateTime()
        {
            // arrange
            const string key = "key1";
            DateTime? value1 = null;
            QueryString queryString = new QueryString();

            // act
            queryString.Add(key, value1);

            // assert
            Assert.Empty(queryString);
        }
    }
}
