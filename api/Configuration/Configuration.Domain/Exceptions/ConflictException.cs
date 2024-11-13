using System;

namespace Configuration.Domain.Exceptions
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message, Exception? innerEx = null, object? @object = null) : base(message, innerEx, @object)
        {
        }
    }
}