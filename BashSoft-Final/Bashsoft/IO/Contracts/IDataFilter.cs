using System.Collections.Generic;

namespace Bashsoft.IO.Contracts
{
    public interface IDataFilter
    {
        void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake);
    }
}
