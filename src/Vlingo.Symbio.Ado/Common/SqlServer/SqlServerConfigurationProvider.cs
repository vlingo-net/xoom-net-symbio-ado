using System;
using System.Data;
using System.Data.SqlClient;

namespace Vlingo.Symbio.Ado.Common.SQLServer
{
    public static class SqlServerConfigurationProvider
    {
        public static ConfigurationInterest Interest => new ConfigurationInterest();
    }

    public class ConfigurationInterest: IConfigurationInterest
    {
        public ConfigurationInterest()
        {
        }

        public void AfterConnect(SqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public void BeforeConnect(Configuration configuration)
        {
            throw new NotImplementedException();
        }

        public void CreateDatabase(SqlConnection sqlConnection, string databaseName)
        {
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    var createDatabaseQuery = "exec ('CREATE DATABASE ' + @databaseName)";
                    var sqlCommand = new SqlCommand(createDatabaseQuery, sqlConnection);
                    sqlCommand.Parameters.Add("@databaseName", SqlDbType.Text);
                    sqlCommand.Parameters["@databaseName"].Value = databaseName;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sql Database {databaseName} count not be created because: {ex.Message}");
                throw;
            }
        }

        public void DropDatabase(SqlConnection sqlConnection, string databaseName)
        {
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    var createDatabaseQuery = "exec ('DROP DATABASE ' + @databaseName)";
                    var sqlCommand = new SqlCommand(createDatabaseQuery, sqlConnection);
                    sqlCommand.Parameters.Add("@databaseName", SqlDbType.Text);
                    sqlCommand.Parameters["@databaseName"].Value = databaseName;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sql Database {databaseName} count not be dropped because: {ex.Message}");
                throw;
            }
        }
    }
}
