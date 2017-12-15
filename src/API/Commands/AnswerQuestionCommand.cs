using MediatR;

namespace API.Commands
{
    public class AnswerQuestionCommand : IRequest<int>
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
    }
}
