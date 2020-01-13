using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security;
using System.Text;

namespace Vlingo.Symbio.Ado.Common
{
    public class ConnectionProvider
    {
        public string DatabaseName { get; private set; }
        public string DriverClassname { get; private set; }
        public string Url { get; private set; }
        public string Username { get; private set; }
        public bool UseSSL { get; private set; }
        public string Password { get; private set; }

        public ConnectionProvider(
                string driverClassname,
                string url,
                string databaseName,
                string username,
                string password,
                bool useSSL)
        {
            DriverClassname = driverClassname;
            Url = url;
            DatabaseName = databaseName;
            Username = username;
            Password = password;
            UseSSL = useSSL;
        }

        /**
         * Answer a new instance of a {@code Connection}.
         * @return Connection
         */
        public SqlConnection Connection()
        {
            try
            {
                var secureString = new SecureString();
                foreach (char c in Password)
                {
                    secureString.AppendChar(c);
                }
                var sqlCredentials = new SqlCredential(Username, secureString);

                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    InitialCatalog = DatabaseName,
                    Encrypt = UseSSL
                };

                var connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString, sqlCredentials);
                return connection;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(this.GetType().Name + ": Cannot connect because database unavailable or wrong credentials.");
            }
        }

        /**
         * Answer a copy of me but with the given {@code databaseName}.
         * @param databaseName the string name of the database with which to create the new ConnectionProvider
         * @return ConnectionProvider
         */
        public ConnectionProvider CopyReplacing(string databaseName)
        {
            return new ConnectionProvider(DriverClassname, Url, databaseName, Username, Password, UseSSL);
        }
    }
}
