using System.Data;
using Dapper;
using GenericSmallBusinessApp.Server.Interfaces;

namespace GenericSmallBusinessApp.Server.Repositories
{
    public class BaseRepository(IDbConnection dbConnection) : IBaseRepository
    {
        public async Task<List<T>> GetData<T, P>(string sql, P parameters)
        {
            using (dbConnection)
            {
                try
                {
                    var data = await dbConnection.QueryAsync<T>(sql, parameters);
                    return data.ToList();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task<T> GetObject<T, P>(string sql, P parameters)
        {
            using (dbConnection)
            {
                try
                {
                    var data = await dbConnection.QueryFirstOrDefaultAsync<T>(sql, parameters);
                    return data;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task SaveData<P>(string sql, P parameters)
        {
            using (dbConnection)
            {
                try
                {
                    var result = await dbConnection.ExecuteAsync(sql, parameters);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }
    }
}
