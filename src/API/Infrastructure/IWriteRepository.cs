using System.Threading.Tasks;
using API.Domain;

namespace API.Infrastructure
{
    public interface IWriteRepository
    {
        Task<Question> GetAsync(int id);
        Task SaveAsync(Question question);
    }
}