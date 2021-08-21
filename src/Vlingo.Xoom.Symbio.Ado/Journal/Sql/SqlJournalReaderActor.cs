// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Streams;
using Vlingo.Xoom.Symbio.Ado.Common;
using Vlingo.Xoom.Symbio.Store.Journal;

namespace Vlingo.Xoom.Symbio.Ado.Journal.Sql
{
    public class SqlJournalReaderActor : Actor, IJournalReader
    {
        private SqlConnection _connection;
        private DatabaseType _databaseType;
        private string _name;

        private long _offset;

        public string Beginning => throw new NotImplementedException();

        public string End => throw new NotImplementedException();

        public string Query => throw new NotImplementedException();

        public int DefaultGapPreventionRetries => throw new NotImplementedException();

        public long DefaultGapPreventionRetryInterval => throw new NotImplementedException();

        public ICompletes<string> Name => throw new NotImplementedException();

        public ICompletes<long> Size => throw new NotImplementedException();
        public ICompletes<IStream> StreamAll()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public ICompletes<IEntry> ReadNext()
        {
            throw new NotImplementedException();
        }

        public ICompletes<IEntry> ReadNext(string fromId)
        {
            throw new NotImplementedException();
        }

        public ICompletes<IEnumerable<IEntry>> ReadNext(int maximumEntries)
        {
            throw new NotImplementedException();
        }

        public ICompletes<IEnumerable<IEntry>> ReadNext(string fromId, int maximumEntries)
        {
            throw new NotImplementedException();
        }

        public void Rewind()
        {
            throw new NotImplementedException();
        }

        public ICompletes<string> SeekTo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
