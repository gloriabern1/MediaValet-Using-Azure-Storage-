using AzureStorageServices.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.Queryable;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageServices.Services
{
    public class TableService : ITableService
    {
        private readonly string storageAccountConnString;
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly string tableName;
        private readonly CloudTableClient tableClient;
        private readonly string partitionKey = "Order";

        public TableService()
        {
            storageAccountConnString = ConfigurationManager.AppSettings["DefaultConnection"];
            cloudStorageAccount = CloudStorageAccount.Parse(storageAccountConnString);
            tableClient = cloudStorageAccount.CreateCloudTableClient();
            tableName = ConfigurationManager.AppSettings["TableName"];


        }

        public async Task<bool> SaveConfirmation(Confirmations confirmations)
        {
            try
            {
                confirmations.PartitionKey = this.partitionKey;
                confirmations.RowKey = confirmations.OrderId.ToString();

                CloudTable cloudTable = tableClient.GetTableReference(tableName);
                _ = cloudTable.CreateIfNotExists();
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(confirmations);
                TableResult result = cloudTable.Execute(insertOrMergeOperation);

                if (result.HttpStatusCode == 201 || result.HttpStatusCode == 204)
                    return true;

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error :{ex.Message}");
                Console.WriteLine($"Error :{ex.StackTrace}");
                return false;
            }
        }

        public async Task<IEnumerable<Confirmations>> GetConfirmationOrders()
        {
            try
            {
                CloudTable cloudTable = tableClient.GetTableReference(tableName);
                if (cloudTable.Exists())
                {
                    TableQuery<Confirmations> confirmationsQuery = cloudTable.CreateQuery<Confirmations>();
                    var query = (from confirmation in confirmationsQuery
                                 where confirmation.PartitionKey == partitionKey
                                 select confirmation).AsTableQuery();

                    var confirmations = query.Execute();
                    return confirmations;
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
    }
}
