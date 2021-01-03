// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Data;
using Vlingo.Symbio.Ado.Common;

namespace Vlingo.Symbio.Ado
{
    public abstract class ConnectionProvider
    {    
        public ConnectionProvider(
            string url,
            string databaseName,
            string username,
            string password,
            bool useSsl)
        {
            Url = url;
            DatabaseName = databaseName;
            Username = username;
            Password = password;
            UseSsl = useSsl;
        }

        public string DatabaseName { get; }
        public string Url { get; }
        public string Username { get; }
        public bool UseSsl { get; }
        public string Password { get; }

        /// <summary>
        ///     Answer a new instance of a <see cref="IDbConnection" />
        /// </summary>
        /// <returns>A instance of <see cref="IDbConnection" /> connection</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public abstract IDbConnection Connection();

        public abstract ConnectionProvider CopyReplacing(string databaseName);
    }
}