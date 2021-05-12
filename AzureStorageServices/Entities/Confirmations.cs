using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageServices.Entities
{
    public class Confirmations : TableEntity
    {
        public Confirmations(int orderId, Guid agentId)
        {
            this.OrderId = orderId;
            this.AgentId = agentId;
            this.OrderStatus = "Processed";
        }
        public Confirmations()
        {

        }

        public int OrderId { get; set; }
        public Guid AgentId { get; set; }
        public string OrderStatus { get; set; }



    }
}
