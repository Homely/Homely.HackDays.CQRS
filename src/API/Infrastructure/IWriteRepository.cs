using System.Threading.Tasks;
using API.Domain;

namespace API.Infrastructure
{
    public interface IWriteRepository
    {
        Task SaveAsync(Question question);
    }
}