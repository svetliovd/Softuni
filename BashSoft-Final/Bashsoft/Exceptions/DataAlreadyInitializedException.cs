using System;

namespace Bashsoft.Exceptions
{
    public class DataAlreadyInitializedException : Exception
    {
        public const string DataAlreadyInitialized = "Data already initialized!";

        public DataAlreadyInitializedException()
            :base(DataAlreadyInitialized)
        {
        }

        public DataAlreadyInitializedException(string message)
            :base(message)
        {
        }
    }
}
