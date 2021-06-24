using System;
using System.Runtime.Serialization;

namespace TransactionUploader.Common.Exceptions
{
    [Serializable]
    public class StorageFailureException : ApplicationException
    {
        
        public StorageFailureException()
        {
        }
        
        public StorageFailureException(string message, Exception innerException)
        :base(message,innerException)
        {
        }
        public StorageFailureException(string message) : base(message)
        {
        }

        protected StorageFailureException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
        
    }
}