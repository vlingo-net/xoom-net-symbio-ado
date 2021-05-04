using System;
using System.Collections.Generic;
using Vlingo.Symbio.Store.State;
using Vlingo.Xoom.Common;

namespace Vlingo.Symbio.Ado.StateStore
{
    public class SqlStateStoreActor<T> : State<T>, IStateStore
    {
        public SqlStateStoreActor(string id, Type type, int typeVersion, T data, int dataVersion, Metadata metadata) : base(id, type, typeVersion, data, dataVersion, metadata)
        {
        }

        public SqlStateStoreActor(string id, Type type, int typeVersion, T data, int dataVersion) : base(id, type, typeVersion, data, dataVersion)
        {
        }

        public ICompletes<IStateStoreEntryReader<TEntry>> EntryReader<TEntry>(string name) where TEntry : IEntry
        {
            return Completes.WithSuccess((IStateStoreEntryReader<TEntry>) new SqlStateStoreEntryReaderActor(/*TODO sql connection*/ null, typeof(T).Name)); 
        }

        public void Read<TState>(string id, IReadResultInterest interest)
        {
            throw new System.NotImplementedException();
        }

        public void Read<TState>(string id, IReadResultInterest interest, object? @object)
        {
            throw new System.NotImplementedException();
        }

        public void ReadAll<TState>(IEnumerable<TypedStateBundle> bundles, IReadResultInterest interest, object? @object)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState>(string id, TState state, int stateVersion, IWriteResultInterest interest)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState, TSource>(string id, TState state, int stateVersion, IEnumerable<TSource> sources, IWriteResultInterest interest)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState>(string id, TState state, int stateVersion, Metadata metadata, IWriteResultInterest interest)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState, TSource>(string id, TState state, int stateVersion, IEnumerable<TSource> sources, Metadata metadata, IWriteResultInterest interest)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState>(string id, TState state, int stateVersion, IWriteResultInterest interest, object @object)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState, TSource>(string id, TState state, int stateVersion, IEnumerable<TSource> sources, IWriteResultInterest interest, object @object)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState>(string id, TState state, int stateVersion, Metadata metadata, IWriteResultInterest interest, object? @object)
        {
            throw new System.NotImplementedException();
        }

        public void Write<TState, TSource>(string id, TState state, int stateVersion, IEnumerable<TSource> sources, Metadata metadata, IWriteResultInterest interest, object? @object)
        {
            throw new System.NotImplementedException();
        }
    }
}