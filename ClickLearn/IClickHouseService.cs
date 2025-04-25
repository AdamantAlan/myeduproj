using ClickHouse.Client.ADO;
using ClickHouse.Client.Copy;
using System.Data;

namespace ClickLearn
{
    public interface IClickHouseService
    {
        Task<DataTable> GetAllAsync();
    }

    public class ClickHouseService(ClickHouseConnection clickHouse) : IClickHouseService
    {
        public async Task<DataTable> GetAllAsync()
        {
            await clickHouse.OpenAsync();

            using var command = clickHouse.CreateCommand();

            command.CommandText = "SELECT * FROM myData";

            using var reader = await command.ExecuteReaderAsync();

            var dataTable = new DataTable();

            dataTable.Load(reader);

            return dataTable;
        }

        public async Task Create()
        {
            await clickHouse.OpenAsync();

            var bulk = new ClickHouseBulkCopy(clickHouse)
            {
                DestinationTableName = "testdata.myData",
                BatchSize = 100000
            };

            var values = Enumerable.Range(0, 1000000)
                .Select(i => new object[] { DateOnly.FromDateTime(DateTime.Now), i.ToString(), i });

            await bulk.WriteToServerAsync(values);
        }

        public async Task InsertDataAsync()
        {
            await clickHouse.OpenAsync();

            string insertQuery = "INSERT INTO users (DateStamp, Name, Value) VALUES (@DateStamp, @Name, @Value)";

            using var command = clickHouse.CreateCommand();

            command.CommandText = insertQuery;
            command.Parameters.Add(DateOnly.FromDateTime(DateTime.Now));
            command.Parameters.Add("John Doe");
            command.Parameters.Add(1);

            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateTableAsync()
        {
            await clickHouse.OpenAsync();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS users (
                    id UInt32,
                    name String,
                    age UInt8
                ) ENGINE = MergeTree()
                ORDER BY id";

            using var command = clickHouse.CreateCommand();
            command.CommandText = createTableQuery;

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteDataAsync()
        {
            await clickHouse.OpenAsync();
            string deleteQuery = @"
                ALTER TABLE users DELETE WHERE DateStamp = '2025-04-12';";

            using var command = clickHouse.CreateCommand();
            command.CommandText = deleteQuery;

            await command.ExecuteNonQueryAsync();
        }
    }

}
