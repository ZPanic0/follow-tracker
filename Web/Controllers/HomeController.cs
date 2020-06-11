using System.Diagnostics;
using System.Threading.Tasks;
using CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.AsyncQueue;
using Services.AsyncQueue.Models;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator mediator;
        private readonly IAsyncQueue<IBackgroundQueueItem> queue;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IAsyncQueue<IBackgroundQueueItem> queue)
        {
            _logger = logger;
            this.mediator = mediator;
            this.queue = queue;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Test()
        {
            return Content(await mediator.Send(new TestQuery.Request()));
        }
    }
}
