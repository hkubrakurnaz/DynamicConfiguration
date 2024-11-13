using System.Collections.Generic;
using System.Linq;
using Configuration.Domain.Exceptions;

namespace Configuration.Api.Models.Responses.Base
{
    public record ErrorResponse(string Message, object? Data = null);

    public record ValidationErrorResponse
    {
        public List<ErrorResponse> Errors { get; private set; }
        
        public ValidationErrorResponse(ValidationException validationException)
        {
            Errors = validationException.ValidationErrors
                .Select(e => new ErrorResponse(e.Message, e.Data))
                .ToList();
        }
    }
}

