using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Web.Services.AsyncQueue
{
    public class AsyncQueue<T> : IAsyncEnumerable<T>, IAsyncQueue<T>
    {
        private readonly SemaphoreSlim _enumerationSemaphore = new SemaphoreSlim(1);
        private readonly BufferBlock<T> _bufferBlock = new BufferBlock<T>();

        public void Enqueue(T item) => _bufferBlock.Post(item);

        public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            await _enumerationSemaphore.WaitAsync();
            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return await _bufferBlock.ReceiveAsync(cancellationToken);
                }
            }
            finally
            {
                _enumerationSemaphore.Release();
            }
        }
    }
}
