using System.Threading.Tasks;
using API.Domain;
using API.Infrastructure;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using AnswerQuestionCommand = API.Commands.AnswerQuestionCommand;

namespace API.Handlers
{
    public class QuestionAnsweredHandler : AsyncRequestHandler<AnswerQuestionCommand, int>
    {
        private readonly CloudQueue _queue;
        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _readRepository;

        public QuestionAnsweredHandler(IConfiguration configuration, IWriteRepository writeRepository,
            IReadRepository readRepository)
        {
            var azureConnectionString = configuration.GetValue<string>("Azure:ConnectionString");
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            _queue = queueClient.GetQueueReference(configuration.GetValue<string>("Azure:QuestionAnsweredQueueName"));
            _queue.CreateIfNotExistsAsync().GetAwaiter().GetResult();

            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        protected override async Task<int> HandleCore(AnswerQuestionCommand request)
        {
            // TODO: Log.

            // Get question.
            var question = await _readRepository.GetAsync(request.QuestionId);

            // TODO: this should come from auth credentials.
            var user = new User("Bob Cobb", "bob@cobb.com");

            // TODO: Validate.

            // Add answer.
            var answer = new Answer(request.Content, user);
            question.Answer(answer);

            // Save.
            await _writeRepository.SaveAsync(question);

            // Add to queue.
            var cloudMessage = new CloudQueueMessage(JsonConvert.SerializeObject(question));
            await _queue.AddMessageAsync(cloudMessage);

            // Return.
            return question.Id;
        }
    }
}