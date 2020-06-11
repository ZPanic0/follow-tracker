using CQRS.Common;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries
{
    public static class GetWebhookSubscriptions
    {
        public class Request : IRequest<IEnumerable<Result>> { }

        public class Result
        {
            public int Id { get; set; }
            public WebhookType WebhookType { get; set; }
            public string Guid { get; set; }
            public string Json { get; set; }
            public string Secret { get; set; }
            public DateTime DateLastUpdated { get; set; }
            public DateTime DateExpires { get; set; }
        }

        public class Handler : IRequestHandler<Request, IEnumerable<Result>>
        {
            private const string sql = @"
                SELECT
                	ws.id,
                	ws.webhooktypeid as webhooktype,
                	u.guid,
                	ws.jsondata as json,
                	ws.secret,
                	ws.datelastupdated,
                	ws.dateexpires
                FROM webhooksubscriptions ws
                INNER JOIN ""user"" u ON u.id = ws.userid;
                ";

            private readonly IDbConnection connection;

            public Handler(IDbConnection connection)
            {
                this.connection = connection;
            }

            public Task<IEnumerable<Result>> Handle(Request request, CancellationToken cancellationToken)
            {
                return connection.QueryAsync<Result>(sql, request);
            }
        }
    }
}
