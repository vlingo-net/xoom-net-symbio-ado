using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Vlingo.Symbio.Ado.Common
{
    public interface IConfigurationInterest
    {
        void AfterConnect(SqlConnection connection);
        void BeforeConnect(Configuration configuration);
        void CreateDatabase(SqlConnection connection, string databaseName);
        void DropDatabase(SqlConnection connection, string databaseName);
    }
}
