using CQRS.Common;
using CQRS.Queries;
using Dapper;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Commands
{
    public class SaveWebhookJsonTests : BaseTest
    {
        [Theory]
        [InlineData(WebhookType.NewFollower)]
        [InlineData(WebhookType.StateChanged)]
        public async Task SavesExpectedValues(WebhookType webhookType)
        {
            var connection = GetConnection();
            var guid = Guid.NewGuid().ToString();
            var json = @"{""A"": ""B""}";

            await connection.ExecuteAsync($@"
                DELETE FROM webhookdumps;
                DELETE FROM ""user"";
                INSERT INTO ""user"" (twitchuserid, guid) VALUES (1, '{guid}');
            ");

            var userId = await connection.QueryFirstAsync<int>(@"
                SELECT id FROM ""user"" LIMIT 1
            ");

            var handler = new SaveWebhookJson.Handler(connection);

            await handler.Handle(new SaveWebhookJson.Request(webhookType, guid, json), CancellationToken.None);

            var result = await connection.QuerySingleAsync(@"
                SELECT * FROM webhookdumps;
            ");

            ((WebhookType)result.webhooktypeid).Should().Be(webhookType);
            ((int)result.userid).Should().Be(userId);
            ((DateTime)result.datecreated).Should().BeCloseTo(DateTime.UtcNow, precision: 1000);
            ((string)result.jsondata).Should().Be(json);
        }
    }
}
