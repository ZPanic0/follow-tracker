using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Services.AsyncQueue;
using Xunit;

namespace Unit
{
    public class AsyncQueueTests
    {
        [Fact]
        public async Task CanQueueAndRetrieveItems()
        {
            var queue = new AsyncQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            var enumerator = queue.GetAsyncEnumerator();

            await enumerator.MoveNextAsync();
            enumerator.Current.Should().Be(1);
            await enumerator.MoveNextAsync();
            enumerator.Current.Should().Be(2);
            await enumerator.MoveNextAsync();
            enumerator.Current.Should().Be(3);
        }

        [Fact]
        public async Task CanBeCancelled()
        {
            var cancellationToken = new CancellationTokenSource();
            var queue = new AsyncQueue<int>();
            var enumerator = queue.GetAsyncEnumerator(cancellationToken.Token);

            Func<Task> action = async () =>
            {
                queue.Enqueue(1);
                queue.Enqueue(2);
                queue.Enqueue(3);

                cancellationToken.Cancel();

                await enumerator.MoveNextAsync();
            };

            await action.Should().ThrowAsync<OperationCanceledException>();
        }
    }
}
