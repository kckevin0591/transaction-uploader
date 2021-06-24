using System;
using System.Runtime.Serialization;

namespace TransactionUploader.Common.Exceptions
{
    [Serializable]
    public class InvalidFileContentException : ApplicationException
    {
        public InvalidFileContentException()
        {
        }
        
        public InvalidFileContentException(string message, Exception innerException)
        :base(message,innerException)
        {
        }
        public InvalidFileContentException(string message) : base(message)
        {
        }

        protected InvalidFileContentException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
        
    }
}