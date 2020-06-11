using CQRS.Commands;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Services.AsyncQueue;
using Web.Services.AsyncQueue.Models;

namespace Web
{
    public class BackgroundService : IHostedService
    {
        private readonly IAsyncQueue<WebhookWorkItem> queue;
        private readonly IMediator mediator;

        public BackgroundService(IAsyncQueue<WebhookWorkItem> queue, IMediator mediator)
        {
            this.queue = queue;
            this.mediator = mediator;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(BackgroundService)} started!");

            await foreach (var item in queue)
            {
                await mediator.Send(new SaveWebhookJson.Request(item.WebhookType, item.Guid, item.Json));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(BackgroundService)} stopping!");

            return Task.CompletedTask;
        }
    }
}
