using System;
using System.Collections.Generic;
using System.Text;
using Vlingo.Actors;
using Vlingo.Common;
using Vlingo.Symbio.Store.Journal;

namespace Vlingo.Symbio.Ado.Journal.Sql
{
    public class SqlJournalActor : Actor, IJournal<string>
    {
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

        public ICompletes<IJournalReader<TNewEntry>?> JournalReader<TNewEntry>(string name) where TNewEntry : IEntry
        {
            throw new NotImplementedException();
        }

        public ICompletes<IStreamReader<string>?> StreamReader(string name)
        {
            throw new NotImplementedException();
        }

        IJournal<string> IJournal<string>.Using<TActor, TEntry, TState>(Stage stage, Store.Dispatch.IDispatcher<Store.Dispatch.Dispatchable<TEntry, TState>> dispatcher, params object[] additional)
        {
            throw new NotImplementedException();
        }
    }
}
