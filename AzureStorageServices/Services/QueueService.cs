using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using AzureStorageServices.Entities;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageServices.Services
{
    public class QueueService : IQueueService
    {

        private readonly string storageAccountConnString;
        private readonly string queueName;
        private readonly QueueClient queueClient;
        public QueueService()
        {
            storageAccountConnString = ConfigurationManager.AppSettings["DefaultConnection"];
            queueName = ConfigurationManager.AppSettings["QueueName"];
            queueClient = new QueueClient(storageAccountConnString, queueName);

        }

        public async Task<bool> PushOrderToQueue(Orders orders)
        {
            try
            {
                UniqueIdGenerator uniqueIdGenerator = new UniqueIdGenerator(2, 5);
                orders.OrderId = int.Parse(uniqueIdGenerator.NextId().ToString());
                queueClient.CreateIfNotExists();
                await queueClient.SendMessageAsync(JsonConvert.SerializeObject(orders), default, TimeSpan.FromSeconds(-1), default);

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error :{ex.Message}");
                Console.WriteLine($"Error :{ex.StackTrace}");
                return false;
            }


        }

        //Get order from queue
        public async Task<QueueMessage[]> ReadOrderFromQueue()
        {
            try
            {


                if (queueClient.Exists())
                {
                    QueueProperties properties =  queueClient.GetProperties();

                    if (properties.ApproximateMessagesCount > 0)
                    {
                        //Set the number to get per call 10 orders
                        QueueMessage[] Messages = queueClient.ReceiveMessages(10);
                        return Messages;

                    }
                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error :{ex.Message}");
                Console.WriteLine($"Error :{ex.StackTrace}");
                return null;
            }


        }

        //Delete order from queue
        public async Task DeleteOrderFromQueue(QueueMessage queueMessage)
        {
            try
            {
                if (queueClient.Exists() && queueClient.PeekMessages().Value.Count() > 0)
                {
                    queueClient.DeleteMessage(queueMessage.MessageId, queueMessage.PopReceipt);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error :{ex.Message}");
                Console.WriteLine($"Error :{ex.StackTrace}");
            }


        }
    }
}
