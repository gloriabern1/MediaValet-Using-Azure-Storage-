using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageServices.Services
{
    public interface IBlobUniqueCheck
    {
        string GetData();
        bool TryOptimisticWrite(string data);
    }
}
