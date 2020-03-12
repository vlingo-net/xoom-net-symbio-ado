// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Data;

namespace Vlingo.Symbio.Ado.Common
{
    public interface IConfigurationInterest
    {
        void AfterConnect(IDbConnection connection);
        void BeforeConnect(Configuration configuration);
        void CreateDatabase(IDbConnection connection, string databaseName);
        void DropDatabase(IDbConnection connection, string databaseName);
    }
}
