using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UISystem.Extensions
{
    public static  class SemaphoreSlimExtensions
    {
        public static async Task<IDisposable> UseWaitAsync( this SemaphoreSlim semaphore, CancellationToken cancelToken = default)
        {
            await semaphore.WaitAsync(cancelToken).ConfigureAwait(false);
            return new ReleaseWrapper(semaphore);
        }
        
        public static void ReleaseIfZero(this SemaphoreSlim sem)
        {
            if (sem.CurrentCount == 0)
                sem.Release();
        }

        class ReleaseWrapper : IDisposable
        {
            readonly SemaphoreSlim _semaphore;

            bool _isDisposed;

            public ReleaseWrapper(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }

            public void Dispose()
            {
                if (_isDisposed)
                    return;

                if (_semaphore.CurrentCount != 0)
                    Debug.LogError($"[SemaphoreSlim] WTF: count = {_semaphore.CurrentCount}");
                _semaphore.ReleaseIfZero();
                _isDisposed = true;
            }
        }
    }
}