using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TransactionUploader.Common.Exceptions
{
    [Serializable]
    public class InvalidFileContentException : ApplicationException
    {
        public List<string> Errors { get; set; }
        public InvalidFileContentException()
        {
            Errors = new List<string>();
        }
        
        public InvalidFileContentException(string message, List<string> errors, Exception innerException)
        :base(message,innerException)
        {
            Errors = errors;
        }
        public InvalidFileContentException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        protected InvalidFileContentException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
        
    }
}