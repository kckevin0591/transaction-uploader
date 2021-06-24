using System;
using System.Runtime.Serialization;

namespace TransactionUploader.Common.Exceptions
{
    [Serializable]
    public class InvalidFileTypeException : ApplicationException
    {
        
        public InvalidFileTypeException()
        {
        }
        
        public InvalidFileTypeException(string message, Exception innerException)
        :base(message,innerException)
        {
        }
        public InvalidFileTypeException(string message) : base(message)
        {
        }

        protected InvalidFileTypeException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
        
    }
}