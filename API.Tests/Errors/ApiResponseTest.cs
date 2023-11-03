using API.Errors;

namespace API.Tests.Errors
{
    public class ApiResponseTest
    {
        [Theory]
        [InlineData(400, "Bad request encountered")]
        [InlineData(401, "Not authorized")]
        [InlineData(404, "No resource was found")]
        [InlineData(500, "Internal server error")]
        [InlineData(null, null)]
        public void GetDefaultMessageTest(int code, string response)
        {
            var apiResponse = new ApiResponse(code, response);
            var result = apiResponse.GetDefaultMessage(code); 

            Assert.Equal(response, result);
        }
    }
}
