using HttpClientExtended.Abstractions;
using HttpClientExtended.Abstractions.Extensions;
using HttpClientExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientExtensions.Abstractions.Test
{
    public class HttpClientQueryBuilderTest
    {
        [Fact]
        public async Task ShouldSetAcceptHeaderInRequest()
        {
            // arrange
            const string requestUri = "/fake";
            const string mediaType = "text/html";
            const string charset = "utf - 8, iso - 8859 - 1; q = 0.5";
            HttpClient client = new HttpClient();
            IHttpClientVerbBuilder<HttpClient> builder = new HttpClientVerbBuilder<HttpClient>(client);

            // act
            HttpRequestMessage httpRequest = await client
                .Request()
                .Get(requestUri)
                .Header("Accept", mediaType)
                .Header("Accept-Charset", charset)
                .BuildHttpRequestAsync();
            MediaTypeWithQualityHeaderValue requestAcceptHeader = httpRequest.Headers.Accept.FirstOrDefault();

            // assert
            Assert.NotNull(requestAcceptHeader);
            Assert.True(requestAcceptHeader.MediaType == mediaType);
            Assert.True(requestAcceptHeader.CharSet == charset);
        }
    }
}
