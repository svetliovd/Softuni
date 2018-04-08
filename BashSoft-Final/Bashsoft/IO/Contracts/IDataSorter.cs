using System.Collections.Generic;

namespace Bashsoft.IO.Contracts
{
    public interface IDataSorter
    {
        void OrderAndTake(Dictionary<string, double> studentsMarks,
            string comparison, int studentsToTake);
    }
}
