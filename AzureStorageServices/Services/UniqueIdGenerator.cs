using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureStorageServices.Services
{
    //Used for getting incremental orderIds
    public class UniqueIdGenerator
    {
        private readonly object _padLock = new object();
        private Int64 _lastId;
        private Int64 _upperLimit;
        private int _rangeSize;
        private int _maxRetries;
        private IBlobUniqueCheck _blobUniqueCheck;

        public UniqueIdGenerator(
            int rangeSize = 1000,
            int maxRetries = 25)
        {
            _rangeSize = rangeSize;
            _maxRetries = maxRetries;
            _blobUniqueCheck = new BlobUniqueCheck();
            UpdateFromSyncStore();
        }

     
        public Int64 NextId()
        {
            lock (_padLock)
            {
                if (_lastId == _upperLimit)
                {
                    UpdateFromSyncStore();
                }
                return Interlocked.Increment(ref _lastId);
            }
        }

        private void UpdateFromSyncStore()
        {
            int retryCount = 0;

            // maxRetries + 1 because the first run isn't a 're'try.
            while (retryCount < _maxRetries + 1)
            {
                string data = _blobUniqueCheck.GetData();

                if (!Int64.TryParse(data, out _lastId))
                {
                    throw new Exception(string.Format(
                       "Data '{0}' in storage was corrupt and could not be parsed as an Int64"
                       , data));
                }

                _upperLimit = _lastId + _rangeSize;

                if (_blobUniqueCheck.TryOptimisticWrite(_upperLimit.ToString()))
                {
                    // update succeeded
                    return;
                }

                retryCount++;
                // update failed, go back around the loop
            }

            throw new Exception(string.Format(
                "Failed to update the OptimisticSyncStore after {0} attempts",
                retryCount));
        }
    }
}
