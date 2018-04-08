namespace Bashsoft.IO.Contracts
{
    public interface IContentComparer
    {
        void CompareContent(string userOutputPath, string expectedOuputPath);
    }
}
