using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.IO;
using Xunit;

namespace Integration
{
    [Collection("Integration")]
    public abstract class BaseTest
    {
        public IDbConnection GetConnection()
        {
            var secrets = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("secrets.json"));
            return new NpgsqlConnection((string)secrets.ConnectionString);
        }
    }
}
