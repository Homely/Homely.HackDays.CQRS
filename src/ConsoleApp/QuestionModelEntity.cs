using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp
{
    public class QuestionModelEntity : TableEntity
    {
        public QuestionModelEntity(int questionId)
        {
            var key = questionId.ToString();

            RowKey = key;
            PartitionKey = key;
        }

        public QuestionModelEntity()
        {
        }

        public string JsonData { get; set; }
    }
}