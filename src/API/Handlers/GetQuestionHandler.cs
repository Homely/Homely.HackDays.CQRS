using System.Threading.Tasks;
using API.Infrastructure;
using API.Models;
using API.Queries;
using MediatR;

namespace API.Handlers
{
    public class GetQuestionHandler : AsyncRequestHandler<GetQuestionQuery, QuestionModel>
    {
        private readonly IReadRepository _readRepository;

        public GetQuestionHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        protected override async Task<QuestionModel> HandleCore(GetQuestionQuery request)
        {
            return await _readRepository.GetSummaryAsync(request.Id);
        }
    }
}