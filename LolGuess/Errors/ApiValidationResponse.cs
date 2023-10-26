namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors
        {
            get; set;
        }
        private static int _errorCode = 400;
        public ApiValidationErrorResponse() : base(_errorCode)
        {

        }
    }
}
