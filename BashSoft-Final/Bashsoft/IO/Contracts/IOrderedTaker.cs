using System.Collections.Generic;

namespace Bashsoft.IO.Contracts
{
    public interface IOrderedTaker
    {
        void OrderAndTake(string courseName, string comparison, int? studentsToTake = default(int?));
    }
}
