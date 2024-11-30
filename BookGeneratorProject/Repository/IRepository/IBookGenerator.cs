using BookGeneratorProject.Model;

namespace BookGeneratorProject.Repository.IRepository
{
    public interface IBookGenerator
    {
        Task<List<Book>> GenerateBooks(int seed, string region, int batchNumber);
    }
}
