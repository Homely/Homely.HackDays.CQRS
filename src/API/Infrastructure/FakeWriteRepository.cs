using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using Domain.Models;

namespace API.Infrastructure
{
    public class FakeWriteRepository : IWriteRepository
    {
        private readonly List<Question> _questions = new List<Question>
        {
            new Question("Best coffee in Melbourne?", new User("Ryan", "ryan@ryan.com")) {Id = 1}
        };

        public Task<Question> GetAsync(int id)
        {
            return Task.FromResult(_questions.SingleOrDefault(q => q.Id == id));
        }

        public async Task SaveAsync(Question question)
        {
            if (question.Id == 0)
            {
                question.Id = _questions.Count + 1;
                _questions.Add(question);
            }
            else
            {
                var existingQuestion = await GetAsync(question.Id);
                if (existingQuestion == null)
                {
                    throw new Exception("Question does not exist.");
                }
                _questions.Remove(existingQuestion);
                _questions.Add(question);
            }
        }
    }
}