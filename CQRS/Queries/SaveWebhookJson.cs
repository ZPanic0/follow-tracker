using CQRS.Common;
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries
{
    public static class SaveWebhookJson
    {
        public class Request : IRequest<int>
        {
            public Request(WebhookType webhookType, string guid, string json)
            {
                WebhookType = webhookType;
                Guid = guid;
                Json = json;
            }

            public WebhookType WebhookType { get; }
            public string Guid { get; }
            public string Json { get; }
        }

        public class Handler : IRequestHandler<Request, int>
        {
            private const string sql = @"
                INSERT INTO webhookdumps (webhooktypeid, userid, jsondata)
                SELECT @WebhookType, ""user"".id, '{0}'
                FROM ""user""
                WHERE ""user"".guid = @Guid
                LIMIT 1";
            private readonly IDbConnection connection;

            public Handler(IDbConnection connection)
            {
                this.connection = connection;
            }

            public Task<int> Handle(Request request, CancellationToken cancellationToken)
            {
                return connection.ExecuteAsync(string.Format(sql, request.Json), request);
            }
        }
    }


}
