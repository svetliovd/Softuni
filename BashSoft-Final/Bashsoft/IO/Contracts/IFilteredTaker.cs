namespace Bashsoft.IO.Contracts
{
    public interface IFilteredTaker
    {
        void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = default(int?));
    }
}
