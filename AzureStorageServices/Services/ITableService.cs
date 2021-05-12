using AzureStorageServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageServices.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Confirmations>> GetConfirmationOrders();
        Task<bool> SaveConfirmation(Confirmations confirmations);
    }
}
