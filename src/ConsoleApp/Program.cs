using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Configuration;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var appSettings = GetAppSettingsConfiguration();

            // Azure storage queue client.
            var client = new AzureStorageClient(appSettings.AzureStorage);

            // Check the queue and sleep for 10 seconds when there's nothing available
            while (true)
            {
                var questionMessage = await client.GetQuestionFromQueueAsync();
                if (questionMessage == null)
                {
                    Console.WriteLine("No message on the queue. Sleeping for 10 secs.");
                    Thread.Sleep(1000 * 10); // 10 secs wait.
                }
                else
                {
                    Console.WriteLine($"Message retrieved from queue: Question Id: {questionMessage.Question.Id}.");

                    // Lets store this question to our own db.
                    Console.Write("Storing projected question model into storage table ...");
                    await client.StoreQuestionToTableStorageAsync(questionMessage.Question);
                    Console.WriteLine(" done.");

                    // Clean up :)
                    Console.Write("Deleting queue message ...");
                    await client.DeleteQuestionFromQueueAsync(questionMessage.MessageId, questionMessage.PopReceipt);
                    Console.WriteLine(" done.");
                }
            }
        }

        private static AppSettings GetAppSettingsConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", false, true)
                .Build();
            
            var settings = new AppSettings();
            configuration.Bind(settings);

            return settings;
        }
    }
}
