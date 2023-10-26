namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        public string GetDefaultMessage(int statusCode)
        {
            if (statusCode == 0)
                return string.Empty;

            return statusCode switch
            {
                400 => "Bad request encountered",
                401 => "Not authorized",
                404 => "No resource was found",
                500 => "Internal server error",
                _ => null
            } ?? "Null reference";
        }

    }
}
