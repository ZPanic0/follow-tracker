using CQRS.Common;
using CQRS.Queries;
using Dapper;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Queries
{
    public class GetWebhookSubscriptionsTests : BaseTest
    {
        [Fact]
        public async Task ReturnsExpectedValues()
        {
            var twitchUserId = 1;
            var guid = Guid.NewGuid().ToString();
            var json = @"{""A"": ""B""}";
            var secret = new Guid().ToString();
            var handler = new GetWebhookSubscriptions.Handler(Connection);

            var sql = $@"
                INSERT INTO ""user"" (twitchuserid, guid) values ({twitchUserId}, '{guid}');
                INSERT INTO webhooksubscriptions (webhooktypeid, userid, jsondata, secret, datelastupdated, dateexpires)
                SELECT 0, u.id, '{json}', {secret}, timezone('utc', now()), timezone('utc', now() + INTERVAL '7 days')
                FROM ""user"" u
                WHERE guid = '{guid}';
                INSERT INTO webhooksubscriptions (webhooktypeid, userid, jsondata, secret, datelastupdated, dateexpires)
                SELECT 1, u.id, '{json}', {secret}, timezone('utc', now()), timezone('utc', now() + INTERVAL '7 days')
                FROM ""user"" u
                WHERE guid = '{guid}';
            ";

            await Connection.ExecuteAsync(sql);

            var results = (await handler.Handle(new GetWebhookSubscriptions.Request(), CancellationToken.None)).ToList();

            results.Count.Should().Be(2);

            results.Any(result => result.WebhookType == WebhookType.NewFollower).Should().BeTrue();
            results.Any(result => result.WebhookType == WebhookType.StateChanged).Should().BeTrue();
            results.First().Should().BeEquivalentTo(results.Last(), options => options
                .Excluding(result => result.WebhookType)
                .Excluding(result => result.Id));
        }
    }
}
