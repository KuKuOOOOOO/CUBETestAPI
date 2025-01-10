using CUBETestAPI.Models.TransferModels;
using CUBETestAPI.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CUBETestAPI.Repository.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<CurrencyNameMappingModel>> GetAllCurrencyNameMapping()
        {
            const string cmdTxt = $@"
SELECT *
FROM CurrencyNameMapping
";
            using IDbConnection con = CreateConnection();
            return await con.QueryAsync<CurrencyNameMappingModel>(cmdTxt);
        }
        public async Task<CurrencyNameMappingModel> GetCurrencyNameMapping(Guid id)
        {
            const string cmdTxt = $@"
SELECT *
FROM CurrencyNameMapping
WHERE ID = @ID
";

            using IDbConnection con = CreateConnection();
            return await con.QueryFirstOrDefaultAsync<CurrencyNameMappingModel>(cmdTxt, new
            {
                ID = id
            });
        }
        public async Task<Guid> CreateCurrencyNameMapping(CurrencyNameMappingModel model)
        {
            const string cmdTxt = $@"
INSERT INTO CurrencyNameMapping(ID, 
                                Currency,
                                ChineseName)
OUTPUT INSERTED.ID
VALUES(@ID,
       @Currency,
       @ChineseName)
";
            using IDbConnection con = CreateConnection();
            return await con.QuerySingleAsync<Guid>(cmdTxt, model);
        }
        public async Task<int> UpdateCurrencyNameMapping(CurrencyNameMappingModel model)
        {
            const string cmdTxt = $@"
UPDATE CurrencyNameMapping
SET Currency = @Currency,
    ChineseName = @ChineseName
WHERE ID = @ID
";
            using IDbConnection con = CreateConnection();
            return await con.ExecuteAsync(cmdTxt, model);
        }
        public async Task<int> DeleteCurrencyNameMapping(Guid id)
        {
            const string cmdTxt = $@"
DELETE FROM CurrencyNameMapping
WHERE ID = @ID
";
            using IDbConnection con = CreateConnection();
            return await con.ExecuteAsync(cmdTxt, new
            {
                ID = id
            });
        }
    }
}
