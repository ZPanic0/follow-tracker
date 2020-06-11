using Dapper;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Integration
{
    public class DatabaseConnectionTests : BaseTest
    {
        [Fact]
        public async Task CanQueryDatabase()
        {
            (await GetConnection().QueryFirstAsync<int>("SELECT 1;")).Should().Be(1);
        }
    }
}
