using Dapper;
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
        public IDbConnection Connection { get; }

        public BaseTest()
        {
            var secrets = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("secrets.json"));
            Connection = new NpgsqlConnection((string)secrets.ConnectionString);
            ClearTables();
        }

        private void ClearTables() => Connection.Execute(@"
            DELETE FROM webhookdumps;
            DELETE FROM ""user"";
        ");
    }
}
