using System;

namespace Configuration.Domain.Exceptions
{
    public abstract class CustomException : Exception
    {
        public object? Object { get; private set; }

        protected CustomException(string message, Exception? innerEx = null, object? @object = null)
            : base(message, innerEx)
        {
            Object = @object;
        }
    } 
}

