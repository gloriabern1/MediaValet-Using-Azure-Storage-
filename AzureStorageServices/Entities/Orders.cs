using AzureStorageServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageServices.Entities
{
    //Orders Entity
    public class Orders
    {
        public int OrderId { get; set; }
        public int RandomNumber { get; set; }
        public string OrderText { get; set; }

        //Should be done with dependency Injection for better performance
        private readonly IQueueService queueService = new QueueService();

        public Orders( string orderText)
        {
            this.OrderText = orderText;
            this.RandomNumber = new Random().Next(1, 10);
        }
        public Orders()
        {
        }
        public async Task<bool> ProcessOrder()
        {
          return  await queueService.PushOrderToQueue(this);
        }
    }
}
