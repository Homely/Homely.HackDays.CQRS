using System.Collections.Generic;

namespace ConsoleApp
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public UserWhoAsked UserWhoAsked { get; set; }
        public List<Answer> Answers { get; set; }
    }
}