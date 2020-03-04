// Copyright © 2012-2020 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Data;
using System.Data.SqlClient;

namespace Vlingo.Symbio.Ado.Common.SQLServer
{
    public class SqlServerConfigurationProvider : IConfigurationInterest
    {
        public static IConfigurationInterest Interest => new SqlServerConfigurationProvider();
        
        public void AfterConnect(IDbConnection connection)
        {
        }

        public void BeforeConnect(Configuration configuration)
        {
        }

        public void CreateDatabase(IDbConnection sqlConnection, string databaseName)
        {
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    var createDatabaseQuery = "exec ('CREATE DATABASE ' + @databaseName)";
                    var sqlCommand = new SqlCommand(createDatabaseQuery, (SqlConnection)sqlConnection);
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

        public void DropDatabase(IDbConnection sqlConnection, string databaseName)
        {
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    var createDatabaseQuery = "exec ('DROP DATABASE ' + @databaseName)";
                    var sqlCommand = new SqlCommand(createDatabaseQuery, (SqlConnection)sqlConnection);
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
