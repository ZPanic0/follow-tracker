using Dapper;
using FluentAssertions;
using Xunit;

namespace Integration
{
    public class DatabaseConnectionTests : BaseTest
    {
        [Fact]
        public void CanQueryDatabase()
        {
            Connection.QueryFirst<int>("SELECT 1;").Should().Be(1);
        }
    }
}
