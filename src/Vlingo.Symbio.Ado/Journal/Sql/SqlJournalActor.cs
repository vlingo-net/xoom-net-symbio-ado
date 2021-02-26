// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Actors;
using Vlingo.Common;
using Vlingo.Symbio.Store.Dispatch;
using Vlingo.Symbio.Store.Journal;

namespace Vlingo.Symbio.Ado.Journal.Sql
{
    public class SqlJournalActor : Actor, IJournal<string>
    {
        public IJournal<string> Using<TActor, TEntry, TState>(Stage stage, IEnumerable<IDispatcher<Dispatchable<TEntry, TState>>> dispatchers, params object[] additional) where TActor : Actor where TEntry : IEntry<string> where TState : class, IState
        {
            throw new NotImplementedException();
        }

        public void Append<TSource, TSnapshotState>(string streamName, int streamVersion, TSource source, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void Append<TSource, TSnapshotState>(string streamName, int streamVersion, TSource source, Metadata metadata, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void AppendAll<TSource, TSnapshotState>(string streamName, int fromStreamVersion, IEnumerable<TSource> sources, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void AppendAll<TSource, TSnapshotState>(string streamName, int fromStreamVersion, IEnumerable<TSource> sources, Metadata metadata, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void AppendAllWith<TSource, TSnapshotState>(string streamName, int fromStreamVersion, IEnumerable<TSource> sources, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void AppendAllWith<TSource, TSnapshotState>(string streamName, int fromStreamVersion, IEnumerable<TSource> sources, Metadata metadata, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void AppendWith<TSource, TSnapshotState>(string streamName, int streamVersion, TSource source, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : Source
        {
            throw new NotImplementedException();
        }

        public void AppendWith<TSource, TSnapshotState>(string streamName, int streamVersion, TSource source, Metadata metadata, TSnapshotState snapshot, IAppendResultInterest interest, object @object) where TSource : Source
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

        IJournal<string> IJournal<string>.Using<TActor, TEntry, TState>(Stage stage, IDispatcher<Dispatchable<TEntry, TState>> dispatcher, params object[] additional)
        {
            throw new NotImplementedException();
        }
    }
}