using Domain.Models;

namespace API.Domain
{
    public class Answer
    {
        public Answer(string content, User userWhoAsked)
        {
            Content = content;
            UserWhoAsked = userWhoAsked;
        }
        
        public string Content { get; }
        public User UserWhoAsked { get; }

        public void Validate()
        {
            // do something domain-like and useful.
        }
    }
}