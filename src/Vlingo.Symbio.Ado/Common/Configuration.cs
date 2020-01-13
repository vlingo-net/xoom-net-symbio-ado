// Copyright © 2012-2020 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Vlingo.Common;
using Vlingo.Symbio.Ado.Common;
using Vlingo.Symbio.Ado.Common.SQLServer;
using Vlingo.Symbio.Store;

namespace Vlingo.Symbio.Ado.Common
{
    public class Configuration
    {
        public static long DefaultTransactionTimeout = 5 * 60 * 1000L; // 5 minutes

        public string actualDatabaseName;
        public SqlConnection Connection;
        public ConnectionProvider ConnectionProvider;
        public DatabaseType databaseType;
        public DataFormat format;
        public string originatorId;
        public bool createTables;
        public long transactionTimeoutMillis;

        protected ConfigurationInterest Interest;

        public static Configuration CloneOf(Configuration other)
        {
            try
            {
                return new Configuration(other.databaseType, other.Interest,other.format, other.ConnectionProvider.Url,
                    other.actualDatabaseName, other.ConnectionProvider.Username, other.ConnectionProvider.Password,
                    other.ConnectionProvider.UseSsl, other.originatorId, other.createTables, other.transactionTimeoutMillis, true);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cannot clone the configuration for " + other.ConnectionProvider.Url + " because: " + e.Message, e);
            }
        }

        public static ConfigurationInterest InterestOf(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.MySql:
                    break;
                case DatabaseType.SqlServer:
                    return SqlServerConfigurationProvider.Interest;
                case DatabaseType.Postgres:
                    break;
            }

            throw new InvalidOperationException("Database currently not supported: " + databaseType.ToString());
        }

        public Configuration(
                 DatabaseType databaseType,
                 ConfigurationInterest interest,
                 DataFormat format,
                 String url,
                 String databaseName,
                 String username,
                 String password,
                 bool useSSL,
                 String originatorId,
                 bool createTables)
        {
            /*this(databaseType, interest, driverClassname, format, url, databaseName, username, password,
                    useSSL, originatorId, createTables, DefaultTransactionTimeout);*/
        }

        public Configuration(
                 DatabaseType databaseType,
                 ConfigurationInterest interest,
                 DataFormat format,
                 String url,
                 String databaseName,
                 String username,
                 String password,
                 bool useSSL,
                 String originatorId,
                 bool createTables,
                 long transactionTimeoutMillis)
        {
            /*this(databaseType, interest, driverClassname, format, url, databaseName, username, password,
                    useSSL, originatorId, createTables, DefaultTransactionTimeout, false);*/
        }

        private Configuration(
                 DatabaseType databaseType,
                 ConfigurationInterest interest,
                 DataFormat format,
                 String url,
                 String databaseName,
                 String username,
                 String password,
                 bool useSSL,
                 String originatorId,
                 bool createTables,
                 long transactionTimeoutMillis,
                 bool reuseDatabaseName)
        {
            /*
                this.databaseType = databaseType;
                this.interest = interest;
                this.format = format;
                this.connectionProvider = new ConnectionProvider(driverClassname, url, databaseName, username, password, useSSL);
                this.actualDatabaseName = reuseDatabaseName ? databaseName : ActualDatabaseName(databaseName);
                this.originatorId = originatorId;
                this.createTables = createTables;
                this.transactionTimeoutMillis = transactionTimeoutMillis;
                beforeConnect();
                this.connection = connect();
                afterConnect();*/
        }

        protected String ActualDatabaseName(String databaseName)
        {
            return ConnectionProvider.DatabaseName;
        }

        protected void AfterConnect()
        {
            Interest.AfterConnect(Connection);
        }

        protected void BeforeConnect()
        {
            Interest.BeforeConnect(this);
        }
        protected SqlConnection Connect()
        {
            return ConnectionProvider.Connection();
        }
    }
}
