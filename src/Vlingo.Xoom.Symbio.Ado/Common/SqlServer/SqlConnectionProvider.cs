// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace Vlingo.Symbio.Ado.Common.SqlServer
{
    /// <summary>
    /// Provider of <see cref="IDbConnection"/> instances.
    /// </summary>
    public class SqlConnectionProvider : ConnectionProvider
    {
        public SqlConnectionProvider(
            string url,
            string databaseName,
            string username,
            string password,
            bool useSsl) : base(url, databaseName, username, password, useSsl)
        {
        }
        
        /// <summary>
        /// Answer a new instance of a <see cref="IDbConnection"/>
        /// </summary>
        /// <returns>A instance of <see cref="IDbConnection"/> connection</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override IDbConnection Connection()
        {
            try
            {
                var secureString = new SecureString();
                foreach (var c in Password)
                {
                    secureString.AppendChar(c);
                }
                var sqlCredentials = new SqlCredential(Username, secureString);

                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    InitialCatalog = DatabaseName,
                    Encrypt = UseSsl
                };

                var connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString, sqlCredentials);
                return connection;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"{GetType().Name}: Cannot connect because database unavailable or wrong credentials.", e);
            }
        }
        
        public override ConnectionProvider CopyReplacing(string databaseName)
        {
            return new SqlConnectionProvider(Url, databaseName, Username, Password, UseSsl);
        }
    }
}