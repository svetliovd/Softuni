using System;
using System.Collections.Generic;

namespace Bashsoft.IO.Contracts
{
    public interface IStudent : IComparable<IStudent>
    {
        string UserName { get; }

        IReadOnlyDictionary<string, ICourse> EnrolledCourses { get; }

        IReadOnlyDictionary<string, double> MarksByCourseName { get; }

        void EnrolllnCourse(ICourse course);

        void SetMarksOnCourse(string courseName, params int[] scores);
    }
}
