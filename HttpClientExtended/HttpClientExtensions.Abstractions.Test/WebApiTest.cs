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
using HttpClientExtended.Abstractions.Extensions;

namespace HttpClientExtensions.Abstractions.Test
{
    public class WebApiTest
    {
        [Fact]
        public async Task ShouldGetSuccessfulHttpResponseWithInt32QueryString()
        {
            // arrange
            const int id = 15;
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
            HttpResponseMessage response = await client
                .Request()
                .Get(requestUri)
                .Query(nameof(id), id)
                .SendAsync();

            // assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);
            var result = await response.Content.ReadAsAsync<FakePayload>(cancellationToken);
            Assert.True(result.Id == id);
        }

        [Fact]
        public async Task ShouldGetSuccessfulHttpResponseWithNonNullableDateTimeQueryString()
        {
            // arrange
            DateTime value = new DateTime(2010, 5, 11, 10, 30, 02, 03);
            const string requestUri = "/fake";
            CancellationToken cancellationToken = CancellationToken.None;
            var webHostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.Run(async context =>
                    {
                        DateTime parsedResult;
                        string r = context.Request.Query.FirstOrDefault(q => q.Key == nameof(value)).Value.FirstOrDefault();
                        DateTime.TryParse(r, out parsedResult);
                        context.Response.ContentType = "application/json";
                        var resultPayload = JsonConvert.SerializeObject(new FakePayload { NonNullDateTime = parsedResult });
                        await context.Response.WriteAsync(resultPayload);
                    });
                });
            TestServer server = new TestServer(webHostBuilder);
            HttpClient client = server.CreateClient();
            IHttpClientVerbBuilder<HttpClient> builder = new HttpClientVerbBuilder<HttpClient>(client);

            // act
            HttpResponseMessage response = await client
                .Request()
                .Get(requestUri)
                .Query(nameof(value), value)
                .SendAsync();

            // assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);
            var result = await response.Content.ReadAsAsync<FakePayload>(cancellationToken);
            Assert.True(result.NonNullDateTime.Year == value.Year);
            Assert.True(result.NonNullDateTime.Month == value.Month);
            Assert.True(result.NonNullDateTime.Day == value.Day);
            Assert.True(result.NonNullDateTime.Hour == value.Hour);
            Assert.True(result.NonNullDateTime.Minute == value.Minute);
            Assert.True(result.NonNullDateTime.Second == value.Second);
            Assert.True(result.NonNullDateTime.Millisecond == value.Millisecond);
        }

        [Fact]
        public async Task ShouldGetSuccessfulHttpResponseWithNonNullableDateTimeOffsetQueryString()
        {
            // arrange
            DateTime datetime = new DateTime(2010, 5, 11, 10, 30, 0, 03);
            DateTimeOffset value = new DateTimeOffset(datetime, TimeSpan.FromHours(8));
            const string requestUri = "/fake";
            CancellationToken cancellationToken = CancellationToken.None;
            var webHostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.Run(async context =>
                    {
                        DateTimeOffset parsedResult;
                        string r = context.Request.Query.FirstOrDefault(q => q.Key == nameof(value)).Value.FirstOrDefault();
                        DateTimeOffset.TryParse(r, out parsedResult);
                        context.Response.ContentType = "application/json";
                        var resultPayload = JsonConvert.SerializeObject(new FakePayload { DateTimeOffset = parsedResult });
                        await context.Response.WriteAsync(resultPayload);
                    });
                });
            TestServer server = new TestServer(webHostBuilder);
            HttpClient client = server.CreateClient();
            IHttpClientVerbBuilder<HttpClient> builder = new HttpClientVerbBuilder<HttpClient>(client);

            // act
            HttpResponseMessage response = await client
                .Request()
                .Get(requestUri)
                .Query(nameof(value), value)
                .SendAsync();

            // assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);
            var result = await response.Content.ReadAsAsync<FakePayload>(cancellationToken);
            Assert.True(result.DateTimeOffset.Year == value.Year);
            Assert.True(result.DateTimeOffset.Month == value.Month);
            Assert.True(result.DateTimeOffset.Day == value.Day);
            Assert.True(result.DateTimeOffset.Hour == value.Hour);
            Assert.True(result.DateTimeOffset.Minute == value.Minute);
            Assert.True(result.DateTimeOffset.Second == value.Second);
            Assert.True(result.DateTimeOffset.Millisecond == value.Millisecond);
            Assert.True(result.DateTimeOffset.Offset.TotalHours == value.Offset.TotalHours);
        }
    }
}
