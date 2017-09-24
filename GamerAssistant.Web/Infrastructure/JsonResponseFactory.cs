namespace GamerAssistant.Web.Infrastructure
{
    public class ValidationError
    {
        public string Key { get; set; }
        public string Error { get; set; }
    }

    public class JsonResponseFactory
    {
        public static object ErrorResponse(ValidationError[] modelErrors, string error)
        {
            return new { Success = false, ModelErrors = modelErrors, ErrorMessage = error };
        }

        public static object ErrorResponse(string error)
        {
            return new { Success = false, ErrorMessage = error };
        }

        public static object SuccessResponse()
        {
            return new { Success = true };
        }

        public static object SuccessResponse(object referenceObject)
        {
            return new { Success = true, Object = referenceObject };
        }
    }
}