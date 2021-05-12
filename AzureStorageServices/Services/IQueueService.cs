using Azure.Storage.Queues.Models;
using AzureStorageServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageServices.Services
{
    public interface IQueueService
    {
        Task DeleteOrderFromQueue(QueueMessage queueMessage);
        Task<bool> PushOrderToQueue(Orders orders);
        Task<QueueMessage[]> ReadOrderFromQueue();
    }
}
