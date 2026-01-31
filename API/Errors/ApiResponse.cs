namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            // Console.WriteLine($"Hello, C# Print! {statusCode} {message}");
            // Console.WriteLine(Environment.StackTrace);
            StatusCode = statusCode;
            Message = message ?? GetDefaultErrorMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        private static string? GetDefaultErrorMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You made a Bad request",
                401 => "You are not authorized",
                404 => "Resource not found, try different one",
                500 => "There is a mess in ourself, let us clean it",
                _   => null
            };
        }
    }
}