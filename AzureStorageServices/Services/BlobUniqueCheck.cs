using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlobRequestOptions = Microsoft.WindowsAzure.StorageClient.BlobRequestOptions;
using CloudBlob = Microsoft.WindowsAzure.StorageClient.CloudBlob;

namespace AzureStorageServices.Services
{
    public class BlobUniqueCheck : IBlobUniqueCheck
    {
        private readonly string storageAccountConnString;
        private readonly string blobName;
        private readonly CloudBlob _cloudBlob;
        public BlobUniqueCheck()
        {
            storageAccountConnString = ConfigurationManager.AppSettings["DefaultConnection"];
            blobName = ConfigurationManager.AppSettings["BlobStorageName"];
            var cloudStorageAccount = CloudStorageAccount.Parse(storageAccountConnString);
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("uniqueidblobs");
            var containerCheck = blobContainer.CreateIfNotExist();
            _cloudBlob = blobContainer.GetBlobReference(blobName);
            if (containerCheck) TryOptimisticWrite("0");
        }

        public string GetData()
        {
            try
            {
                string data = _cloudBlob.DownloadText();
                return data;
            }
            catch (StorageClientException ex)
            {
                return null;
            }

        }

        public bool TryOptimisticWrite(string data)
        {
            try
            {
                _cloudBlob.UploadText(
                    data,
                    Encoding.Default,
                    new BlobRequestOptions
                    {
                        AccessCondition = AccessCondition.IfMatch(_cloudBlob.Properties.ETag)
                    });
            }
            catch (StorageClientException exc)
            {
                if (exc.StatusCode == HttpStatusCode.PreconditionFailed)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
    }
}
