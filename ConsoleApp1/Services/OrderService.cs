using Azure.Storage.Queues.Models;
using AzureStorageServices.Entities;
using AzureStorageServices.Services;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderConsole.Services
{
    public class OrderService
    {
        private readonly IQueueService _queueService;
        private readonly ITableService _tableService;
        public Guid AgentId { get; set; }
        public int MagicNumber { get; set; }
        public OrderService(Guid agentId, int magicNumber)
        {
            _queueService = new QueueService();
            _tableService = new TableService();
            this.AgentId = agentId;
            this.MagicNumber = magicNumber;
        }

        public async Task ProcessAndSaveOrder()
        {
            QueueMessage[] queueMessages = await _queueService.ReadOrderFromQueue();
            if (queueMessages != null)
            {
                for (var i = 0; i < queueMessages.Length; i++)
                {
                    var message = queueMessages[i];
                    Orders orders = message.Body.ToObjectFromJson<Orders>();
                    Console.WriteLine($"Received order {orders.OrderId}");
                    if (orders.RandomNumber == MagicNumber)
                    {
                        Console.WriteLine("Oh no, my magic number was found");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine(orders.OrderText);
                        Confirmations confirmations = new Confirmations(orders.OrderId, AgentId);
                        bool result = await _tableService.SaveConfirmation(confirmations);
                        if (result) await _queueService.DeleteOrderFromQueue(message);
                    }

                }
            }
            else
            {
                Console.WriteLine("No Order to be processed from the queue");

            }

        }
    }
}
