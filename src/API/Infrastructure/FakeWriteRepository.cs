using System;
using System.Threading.Tasks;
using API.Domain;

namespace API.Infrastructure
{
    public class FakeWriteRepository : IWriteRepository
    {
        public Task SaveAsync(Question question)
        {
            if (question.Id == 0)
            {
                question.Id = new Random().Next();
            }

            return Task.CompletedTask;
        }
    }
}
