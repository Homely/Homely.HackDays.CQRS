using System.Threading.Tasks;
using API.Models;

namespace API.Infrastructure
{
    public interface IReadRepository
    {
        Task<QuestionModel> GetSummaryAsync(int id);
    }
}