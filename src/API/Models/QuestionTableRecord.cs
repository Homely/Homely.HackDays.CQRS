using Microsoft.WindowsAzure.Storage.Table;

namespace API.Models
{
    public class QuestionTableRecord : TableEntity
    {
        public QuestionTableRecord(int questionId)
        {
            PartitionKey = questionId.ToString();
            RowKey = questionId.ToString();
        }

        public QuestionTableRecord()
        {   
        }

        public string JsonData { get; set; }
    }
}
