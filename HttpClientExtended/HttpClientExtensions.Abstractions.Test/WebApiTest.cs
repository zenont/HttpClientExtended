using HttpClientExtended.Abstractions;
using HttpClientExtended.Interfaces;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HttpClientExtensions.Abstractions.Test
{
    public class WebApiTest
    {
        [Fact]
        public async Task ShouldGetSuccessfulHttpResponse()
        {
            // arrange
            const int id = 1;
            const string requestUri = "/fake";
            const string idKey = "id";
            CancellationToken cancellationToken = CancellationToken.None;
            var webHostBuilder = new WebHostBuilder()
                .Configure(app => 
                {
                    app.Run(async context => 
                    {
                        int parsedResult = -1;
                        string r = context.Request.Query.FirstOrDefault(q => q.Key == idKey).Value.FirstOrDefault();
                        int.TryParse(r, out parsedResult);
                        context.Response.ContentType = "application/json";
                        var resultPayload = JsonConvert.SerializeObject(new FakePayload { Id = parsedResult });
                        await context.Response.WriteAsync(resultPayload);
                    });
                });
            TestServer server = new TestServer(webHostBuilder);
            HttpClient client = server.CreateClient();
            IHttpClientVerbBuilder<HttpClient> builder = new HttpClientVerbBuilder<HttpClient>(client);

            // act
            HttpResponseMessage response = await builder.Post(requestUri)
                .Query("id", id)
                .SendAsync(cancellationToken);
            
            // assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);
            var result = await response.Content.ReadAsAsync<FakePayload>(cancellationToken);
            Assert.True(result.Id == id);
        }
    }
}
