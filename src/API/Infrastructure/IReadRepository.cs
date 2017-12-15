using System.Threading.Tasks;
using API.Domain;
using API.Models;

namespace API.Infrastructure
{
    public interface IReadRepository
    {
        Task<Question> GetAsync(int id);
        Task<QuestionModel> GetSummaryAsync(int id);
    }
}