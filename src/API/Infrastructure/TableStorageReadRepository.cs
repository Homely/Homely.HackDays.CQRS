using System.Threading.Tasks;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace API.Infrastructure
{
    public class TableStorageReadRepository : IReadRepository
    {
        private readonly CloudTable _db;

        public TableStorageReadRepository(IConfiguration configuration)
        {
            var azureConnectionString = configuration.GetValue<string>("Azure:ConnectionString");
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(configuration.GetValue<string>("Azure:QuestionsTableName"));
            table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
            _db = table;
        }
        
        public async Task<QuestionModel> GetSummaryAsync(int id)
        {
            var retrievedResult =
                await _db.ExecuteAsync(TableOperation.Retrieve<QuestionTableRecord>(id.ToString(), id.ToString()));
            var questionJson = retrievedResult?.Result as QuestionTableRecord;
            var questionModel = JsonConvert.DeserializeObject<QuestionModel>(questionJson.JsonData);
            return questionModel;
        }
    }
}