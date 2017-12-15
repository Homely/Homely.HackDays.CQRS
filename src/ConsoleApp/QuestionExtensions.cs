using System;
using System.Linq;

namespace ConsoleApp
{
    internal static class QuestionExtensions
    {
        internal static QuestionModel ToQuestionModel(this Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            var questionModel = new QuestionModel
            {
                Title = question.Title,
                UserWhoAsked = question.UserWhoAsked.Username,
                Answers = question.Answers.Select(a => $"{a.Content} by {a.UserWhoAsked.Username}")
            };

            return questionModel;
        }
    }
}