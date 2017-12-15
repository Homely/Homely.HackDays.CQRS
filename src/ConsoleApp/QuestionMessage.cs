using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class QuestionMessage
    {
        public QuestionMessage(CloudQueueMessage cloudQueueMessage)
        {
            if (cloudQueueMessage == null)
            {
                throw new System.ArgumentNullException(nameof(cloudQueueMessage));
            }

            MessageId = cloudQueueMessage.Id;
            PopReceipt = cloudQueueMessage.PopReceipt;
            Question = JsonConvert.DeserializeObject<Question>(cloudQueueMessage.AsString);
        }

        public string MessageId { get; }
        public string PopReceipt { get; set; }
        public Question Question { get;}
    }
}