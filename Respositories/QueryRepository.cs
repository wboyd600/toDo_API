using toDo_API.Models;
using Dapper;
using System.Data.SqlClient;

namespace toDo_API.Repositories {
    public interface IGenericRepository
    {
        Task<IEnumerable<T>> QueryReaderAsync<T>(
            string queryText,
            object? parameters = null,
            SqlConnection? connection = null,
            bool useTransaction = false
        );

        Task<int> QueryExecuteAsync(
            string queryText,
            object? parameters = null,
            SqlConnection? connection = null,
            bool useTransaction = false
        );
    }


    public class GenericRepository: IGenericRepository {
                private readonly ApplicationConfig _applicationConfig;
        private string _connectionString => _applicationConfig.ConnectionString;

        public GenericRepository(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public async Task<IEnumerable<T>> QueryReaderAsync<T>(
            string queryText,
            object? parameters = null,
            SqlConnection? connection = null,
            bool useTransaction = false
        )
        {
            try
            {
                if (connection == null)
                {
                    using (connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();

                        return await connection.QueryAsync<T>(queryText, parameters);
                    }
                }

                return await connection.QueryAsync<T>(queryText, parameters);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<int> QueryExecuteAsync(
            string queryText,
            object? parameters = null,
            SqlConnection? connection = null,
            bool useTransaction = false
        )
        {
            try
            {
                if (connection == null)
                {
                    using (connection = new SqlConnection(_connectionString))
                    {
                        if (useTransaction)
                        {
                            await connection.OpenAsync();

                            var transaction = connection.BeginTransaction();

                            try
                            {
                                var affectedRows = await connection.ExecuteAsync(queryText, parameters, transaction);

                                transaction.Commit();

                                return affectedRows;
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();

                                throw e;
                            }
                        }

                        return await connection.ExecuteAsync(queryText, parameters);
                    }
                }

                if (useTransaction)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var affectedRows = await connection.ExecuteAsync(queryText, parameters, transaction);

                            transaction.Commit();

                            return affectedRows;
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();

                            throw e;
                        }
                    }
                }

                return await connection.ExecuteAsync(queryText, parameters);
            }
            catch (Exception e)
            {
                // Throw the exception back up the stack so the API returns a 500.
                throw e;
            }
        }
    }
}