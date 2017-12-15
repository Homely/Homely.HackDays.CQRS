using API.Models;
using MediatR;

namespace API.Queries
{
    public class GetQuestionQuery : IRequest<QuestionModel>
    {
        public int Id { get; set; }
    }
}
