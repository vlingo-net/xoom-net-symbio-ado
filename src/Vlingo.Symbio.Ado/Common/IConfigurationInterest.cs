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
