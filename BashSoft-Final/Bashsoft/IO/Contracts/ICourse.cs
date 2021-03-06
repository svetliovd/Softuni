﻿using System;
using System.Collections.Generic;

namespace Bashsoft.IO.Contracts
{
    public interface ICourse : IComparable<ICourse>
    {
        string Name { get; }

        IReadOnlyDictionary<string, IStudent> StudentsByName { get; }

        void EnrollStudent(IStudent student);
    }
}
