using System.Collections.Generic;
using System.Threading;

namespace Web.Services.AsyncQueue
{
    public interface IAsyncQueue<T>
    {
        void Enqueue(T item);
        IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken token = default);
    }
}
