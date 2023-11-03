using API.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Middleware
{
    public class ExceptionMiddlewareTest
    {
        [Fact]
        public async Task Middleware_NullContentType_Returns200OK()
        {
            var mockNext = new Mock<RequestDelegate>();
            var mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
            var mockHost = new Mock<IHostEnvironment>();

            var middleware = new ExceptionMiddleware(
                mockNext.Object,
                mockLogger.Object,
                mockHost.Object
            );

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);

            Assert.Equal(null, context.Response.ContentType);
            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        }
        [Fact]
        public async Task Middleware_NullRequest_Returns500()
        {
            var mockNext = new Mock<RequestDelegate>();

            var mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
            var mockHost = new Mock<IHostEnvironment>();

            // Arrange
            var middleware = new ExceptionMiddleware(
                null,
                mockLogger.Object,
                mockHost.Object
            );

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();

            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }
    }
}
