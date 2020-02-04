// Copyright © 2012-2020 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Vlingo.Common;
using Vlingo.Symbio.Ado.Common;
using Vlingo.Symbio.Ado.Common.SQLServer;
using Vlingo.Symbio.Store;

namespace Vlingo.Symbio.Ado.Common
{
    /// <summary>
    /// A standard configuration for ADO connections used by
    /// <code>StateStore</code>, <code>Journal</code> and <code>ObjectStore</code>
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// <para>
        /// A default timeout for transactions if not specified by the client.
        /// Note that this is not used as a database timeout, but rather the
        /// timeout between reading some entity/entities from storage and writing
        /// and committing it/them back to storage. This means that user think-time
        /// is built in to the timeout value. The current default is 5 minutes
        /// but can be overridden by using the constructor that accepts the
        /// {@code transactionTimeoutMillis} value. For a practical use of this
        /// see the following implementations, where this timeout represents the
        /// time between creation of the {@code UnitOfWork} and its expiration:
        /// </para>
        /// <para>
        /// <code>Vlingo.Symbio.Store.Object.ObjectStoreDelegate</code>
        /// <code>Vlingo.Symbio.Store.Object.UnitOfWork</code>
        /// </para>
        /// </summary>
        public static long DefaultTransactionTimeout = 5 * 60 * 1000L; // 5 minutes

        private readonly string _actualDatabaseName;
        private IDbConnection _connection;
        private ConnectionProvider _connectionProvider;
        private DatabaseType _databaseType;
        private readonly IConfigurationInterest _interest;
        private DataFormat _format;
        private string _originatorId;
        private bool _createTables;
        private long _transactionTimeoutMillis;

        protected IConfigurationInterest Interest;

        public static Configuration CloneOf(Configuration other)
        {
            try
            {
                return new Configuration(other._databaseType, other.Interest,other._format, other._connectionProvider.Url,
                    other._actualDatabaseName, other._connectionProvider.Username, other._connectionProvider.Password,
                    other._connectionProvider.UseSsl, other._originatorId, other._createTables, other._transactionTimeoutMillis, true);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cannot clone the configuration for " + other._connectionProvider.Url + " because: " + e.Message, e);
            }
        }

        public static IConfigurationInterest InterestOf(DatabaseType databaseType)
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
                 IConfigurationInterest interest,
                 DataFormat format,
                 string url,
                 string databaseName,
                 string username,
                 string password,
                 bool useSsl,
                 string originatorId,
                 bool createTables,
                 long transactionTimeoutMillis)
        {
            /*this(databaseType, interest, driverClassname, format, url, databaseName, username, password,
                    useSSL, originatorId, createTables, DefaultTransactionTimeout);*/
        }

        public Configuration(
                 DatabaseType databaseType,
                 IConfigurationInterest interest,
                 DataFormat format,
                 string url,
                 string databaseName,
                 string username,
                 string password,
                 bool useSsl,
                 string originatorId,
                 bool createTables,
                 long transactionTimeoutMillis,
                 bool reuseDatabaseName)
        {
            _databaseType = databaseType;
            _interest = interest;
            _format = format;
            // _connectionProvider = new ConnectionProvider();
        }

        private Configuration(
                 DatabaseType databaseType,
                 IConfigurationInterest interest,
                 DataFormat format,
                 string url,
                 string databaseName,
                 string username,
                 string password,
                 bool useSSL,
                 string originatorId,
                 bool createTables,
                 long transactionTimeoutMillis,
                 bool reuseDatabaseName, string actualDatabaseName)
        {
            _actualDatabaseName = actualDatabaseName;
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

        protected string ActualDatabaseName(string databaseName)
        {
            return _connectionProvider.DatabaseName;
        }

        protected void AfterConnect()
        {
            Interest.AfterConnect(_connection);
        }

        protected void BeforeConnect()
        {
            Interest.BeforeConnect(this);
        }
        protected IDbConnection Connect()
        {
            return _connectionProvider.Connection();
        }
    }
}
