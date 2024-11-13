using System;

namespace Configuration.Domain.Exceptions
{
    public sealed class NotFoundException : CustomException
    {
        public NotFoundException(string message, Exception? innerEx = null, object? @object = null) : base(message, innerEx, @object)
        {
        }
    }
}

