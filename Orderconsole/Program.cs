using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;

namespace Orderconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionstring = "UseDevelopmentStorage=true";
            var cloudstorageaccount = CloudStorageAccount.DevelopmentStorageAccount;
            var devclient = cloudstorageaccount.CreateCloudQueueClient();
            var queuename = "testq";
            var devq = devclient.GetQueueReference(queuename);
            var output = devq.CreateIfNotExist();
            if (output)
            {
                Console.WriteLine("yay");
            }
            else
            {
                Console.WriteLine("nay");
            }
            
        }
    }
}
