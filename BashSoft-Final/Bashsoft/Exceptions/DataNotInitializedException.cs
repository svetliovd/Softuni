using System;

namespace Bashsoft.Exceptions
{
    public class DataNotInitializedException : Exception
    {
        private const string DataNotInitialized = 
            "The data structure must be initialized first in order to make any operations with it.";

        public DataNotInitializedException()
            :base(DataNotInitialized)
        {
        }

        public DataNotInitializedException(string message)
            :base(message)
        {
        }
    }
}
