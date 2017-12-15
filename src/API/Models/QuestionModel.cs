using System.Collections.Generic;

namespace API.Models
{
    public class QuestionModel
    {
        public string Title { get; set; }
        public string UserWhoAsked { get; set; }
        public IEnumerable<string> Answers { get; set; }
    }
}
