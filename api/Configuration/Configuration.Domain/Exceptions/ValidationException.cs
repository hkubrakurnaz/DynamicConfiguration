using System.Collections.Generic;

namespace Configuration.Domain.Exceptions
{
    public sealed class ValidationException : CustomException
    {
        public List<ErrorMessage> ValidationErrors { get; }
    
        public ValidationException(List<ErrorMessage> validationErrors) : base(string.Empty)
        {
            ValidationErrors = validationErrors;
        }
    }

    public record ErrorMessage(string Message, object? Data = null);
}

