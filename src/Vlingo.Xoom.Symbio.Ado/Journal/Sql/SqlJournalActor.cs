// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Symbio.Ado.Journal.Sql
{
    public class SqlJournalActor : Actor, IJournal<string>
    {
        public IJournal<string> Using<TActor>(Stage stage, IEnumerable<IDispatcher> dispatchers, params object[] additional) where TActor : Actor
        {
            throw new NotImplementedException();
        }

        public void Append<TSource>(string streamName, int streamVersion, TSource source, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void Append<TSource>(string streamName, int streamVersion, TSource source, Metadata metadata, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void AppendAll<TSource>(string streamName, int fromStreamVersion, IEnumerable<ISource> sources, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void AppendAll<TSource>(string streamName, int fromStreamVersion, IEnumerable<ISource> sources, Metadata metadata, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void AppendAllWith<TSource, TSnapshotState>(string streamName, int fromStreamVersion, IEnumerable<ISource> sources, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void AppendAllWith<TSource, TSnapshotState>(string streamName, int fromStreamVersion, IEnumerable<ISource> sources, Metadata metadata, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void AppendWith<TSource, TSnapshotState>(string streamName, int streamVersion, TSource source, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public void AppendWith<TSource, TSnapshotState>(string streamName, int streamVersion, TSource source, Metadata metadata, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : ISource
        {
            throw new NotImplementedException();
        }

        public ICompletes<IJournalReader<IEntry>?> JournalReader(string name)
        {
            throw new NotImplementedException();
        }

        public ICompletes<IStreamReader?> StreamReader(string name)
        {
            throw new NotImplementedException();
        }

        IJournal<string> IJournal<string>.Using<TActor>(Stage stage, IDispatcher dispatcher, params object[] additional)
        {
            throw new NotImplementedException();
        }
    }
}