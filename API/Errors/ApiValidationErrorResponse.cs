namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = [];
        }

        public IEnumerable<string> Errors { get; set; }
    }
}