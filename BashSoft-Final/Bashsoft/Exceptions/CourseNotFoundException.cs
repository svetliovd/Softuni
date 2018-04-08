using System;

namespace Bashsoft.Exceptions
{
    public class CourseNotFoundException :Exception
    {
        private const string CourseNotFound = "Can not set marks. Course not found.";

        public CourseNotFoundException()
            :base(CourseNotFound)
        {
        }
        public CourseNotFoundException(string message)
            :base(message)
        {
        }
    }
}
