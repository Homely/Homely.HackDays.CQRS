using System.Collections.Generic;
using Domain.Models;

namespace API.Domain
{
    public class Question
    {
        public Question(string title, User userWhoAsked)
        {
            Title = title;
            UserWhoAsked = userWhoAsked;
            Answers = new List<Answer>();
        }

        public int Id { get; set; }
        public string Title { get; }
        public User UserWhoAsked { get; }
        public List<Answer> Answers { get; }

        public void AddAnswer(Answer answer)
        {
            answer.Validate();
            Answers.Add(answer);
        }
    }
}