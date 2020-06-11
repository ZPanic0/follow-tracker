using MediatR;
using Microsoft.Extensions.Hosting;
using Services.AsyncQueue;
using Services.AsyncQueue.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Web
{
    public class BackgroundService : IHostedService
    {
        private readonly IAsyncQueue<IBackgroundQueueItem> queue;
        private readonly IMediator mediator;

        public BackgroundService(IAsyncQueue<IBackgroundQueueItem> queue, IMediator mediator)
        {
            this.queue = queue;
            this.mediator = mediator;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(BackgroundService)} started!");

            await foreach (var item in queue)
            {
                await mediator.Send(item);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(BackgroundService)} stopping!");

            return Task.CompletedTask;
        }
    }
}
