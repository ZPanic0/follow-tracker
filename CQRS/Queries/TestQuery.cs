using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries
{
    public static class TestQuery
    {
        public class Request : IRequest<string> { }

        public class Handler : IRequestHandler<Request, string>
        {
            public Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult("Success!");
            }
        }
    }
}
