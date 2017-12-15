using System;
using System.Threading.Tasks;
using ConsoleApp.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class AzureStorageClient
    {
        private readonly CloudQueue _cloudQueue;
        private readonly CloudTable _cloudTable;

        public AzureStorageClient(AzureStorage azureStorage)
        {
            if (azureStorage == null)
            {
                throw new ArgumentNullException(nameof(azureStorage));
            }

            // Retrieve the storage account.
            var storageAccount = CloudStorageAccount.Parse(azureStorage.ConnectionString);

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            _cloudQueue = queueClient.GetQueueReference(azureStorage.QueueName);

            // Create the Table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            _cloudTable = tableClient.GetTableReference(azureStorage.TableName);
        }

        public async Task<QuestionMessage> GetQuestionFromQueueAsync()
        {
            var message = await _cloudQueue.GetMessageAsync();
            return message == null 
                ? null 
                : new QuestionMessage(message);
        }

        public Task DeleteQuestionFromQueueAsync(string messageId, string popReceipt)
        {
            if (string.IsNullOrWhiteSpace(messageId))
            {
                throw new ArgumentException(nameof(messageId));
            }

            if (string.IsNullOrWhiteSpace(popReceipt))
            {
                throw new ArgumentException(nameof(popReceipt));
            }

            return _cloudQueue.DeleteMessageAsync(messageId, popReceipt);
        }

        public Task StoreQuestionToTableStorageAsync(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            // Project the question to a format which _we_ like.
            var questionModel = question.ToQuestionModel();

            // Store this data into TS.
            var json = JsonConvert.SerializeObject(questionModel);
            var questionModelEntity = new QuestionModelEntity(question.Id)
            {
                JsonData = json
            };
            var tableOperation = TableOperation.InsertOrReplace(questionModelEntity);

            return _cloudTable.ExecuteAsync(tableOperation);
        }
    }
}
