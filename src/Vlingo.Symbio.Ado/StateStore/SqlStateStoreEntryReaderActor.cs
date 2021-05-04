using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Vlingo.Symbio.Ado.Common;
using Vlingo.Symbio.Store;
using Vlingo.Symbio.Store.State;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;

namespace Vlingo.Symbio.Ado.StateStore
{
    public class SqlStateStoreEntryReaderActor : Actor, IStateStoreEntryReader<IEntry<string>>
    {
        private readonly SqlCommand queryBatch;
        private readonly SqlCommand queryIds;
        private readonly SqlCommand queryCount;
        private readonly SqlCommand queryLatestOffset;
        private readonly SqlCommand queryOne;
        private readonly SqlCommand updateCurrentOffsetCmd;
        private readonly string name;
        private readonly SqlConnection connection;
        private readonly EntryAdapterProvider entryAdapterProvider;
        
        private long currentId = 0;

        public string Beginning => EntryReader.Beginning;

        public string End => EntryReader.End;

        public string Query => EntryReader.Query;

        public int DefaultGapPreventionRetries => EntryReader.DefaultGapPreventionRetries;

        public long DefaultGapPreventionRetryInterval => EntryReader.DefaultGapPreventionRetryInterval;

        public ICompletes<string> Name => Completes().With(name);

        public ICompletes<long> Size => Completes().With(CountQuery());
        
        public SqlStateStoreEntryReaderActor(SqlConnection connection, string name)
        {
            this.connection = connection;
            this.name = name;
            entryAdapterProvider = EntryAdapterProvider.Instance(Stage.World);
            currentId = 0;
            
            queryBatch = new SqlCommand(@"select ????? from table", connection) {CommandType = CommandType.Text};
            queryCount = new SqlCommand(@"select count(0) from table", connection) { CommandType = CommandType.Text };
            queryIds = new SqlCommand(@"select id from table", connection) { CommandType = CommandType.Text };
            queryLatestOffset = new SqlCommand(@"?????", connection) { CommandType = CommandType.Text };
            queryOne = new SqlCommand(@"select ????? from table where id=:id", connection) { CommandType = CommandType.Text };
            updateCurrentOffsetCmd = new SqlCommand(@"update ?????", connection) {CommandType = CommandType.Text };
        }

        public void Close()
        {
            connection.Close();
        }

        public ICompletes<IEntry<string>> ReadNext()
        {
            try
            {
                queryOne.Parameters.Clear();
                queryOne.Parameters.AddWithValue("currentId", currentId);
                var result = queryOne.ExecuteReader(CommandBehavior.SingleRow);
                if (result.Read())
                {
                    var entry = entryFrom(result);
                    ++currentId;
                    return Completes().With(entry);
                }
                
                // TODO Gap treatment
                return Completes().With<IEntry<string>>(null);
            }
            catch (Exception e)
            {
                Logger.Error($"Unable to read next entry for {name} because: {e.Message}", e);
                return Completes().With<IEntry<string>>(null);
            }
        }

        public ICompletes<IEntry<string>> ReadNext(string fromId)
        {
            SeekTo(fromId);
            return ReadNext();
        }

        public ICompletes<IEnumerable<IEntry<string>>> ReadNext(int maximumEntries)
        {
            try
            {
                queryBatch.Parameters.Clear();
                queryBatch.Parameters.AddWithValue("currentId", currentId);
                queryBatch.Parameters.AddWithValue("maximumEntries", maximumEntries);
                
                currentId += maximumEntries;

                var result = queryBatch.ExecuteReader(CommandBehavior.SingleResult);
                if (result is null) 
                    return Completes().With<IEnumerable<IEntry<string>>>(Array.Empty<IEntry<string>>());
                
                var entries = new List<IEntry<string>>();
                while (result.Read())
                {
                    entries.Add(entryFrom(result));
                }

                // TODO Gap treatment
                return Completes().With<IEnumerable<IEntry<string>>>(entries);
            }
            catch (Exception e)
            {
                Logger.Error($"Unable to read next entry for {name} because: {e.Message}", e);
                return Completes().With<IEnumerable<IEntry<string>>>(Array.Empty<IEntry<string>>());
            }
        }

        public ICompletes<IEnumerable<IEntry<string>>> ReadNext(string fromId, int maximumEntries)
        {
            SeekTo(fromId);
            return ReadNext(maximumEntries);
        }

        public void Rewind()
        {
            currentId = 0;
        }

        public ICompletes<string> SeekTo(string id)
        {

            if (id == Beginning)
            {
                currentId = 1;
                UpdateCurrentOffset();
            }
            else if (id == End)
            {
                currentId = RetrieveLatestOffset() + 1;
                UpdateCurrentOffset();
            }
            else if (id != Query)
            {
                currentId = long.Parse(id);
                UpdateCurrentOffset();
            }

            return Completes().With(currentId.ToString());
        }

        private long RetrieveLatestOffset()
        {
            try
            {
                queryBatch.Parameters.Clear();
                queryLatestOffset.Parameters.AddWithValue("name", name);

                var result = queryLatestOffset.ExecuteScalar();
                if (result is not null)
                {
                    return long.Parse(result.ToString());
                }
            }
            catch (Exception e)
            {
                Logger.Error("Could not retrieve latest offset, using current.");
                Logger.Error(e.Message, e);
            }

            return 0;
        }

        private void UpdateCurrentOffset()
        {
            try
            {
                updateCurrentOffsetCmd.Parameters.Clear();
                updateCurrentOffsetCmd.Parameters.AddWithValue("currentId", currentId);
                updateCurrentOffsetCmd.Parameters.AddWithValue("name", name);

                updateCurrentOffsetCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Error("Could not persist the offset. Will retry on next read.");
                Logger.Error(e.Message, e);
            }
        }

        private long CountQuery()
        {
            try
            {
                var result = queryCount.ExecuteScalar();
                if (result is null) return -1;
                return long.Parse(result.ToString());
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return -1L;
            }
        }

        private IEntry<string> entryFrom(SqlDataReader result)
        {
            var id = Convert.ToString(result.GetInt64(1));
            var entryData = result.GetString(2);
            var entryType = result.GetString(3);
            var eventTypeVersion = result.GetInt32(4);
            var entryMetadata = result.GetString(5);
            var entryVersion = result.GetInt32(6);
            var classOfEvent = StoredTypes.ForName(entryType);

            var metadata = JsonConvert.DeserializeObject<Metadata>(entryMetadata);
            return new TextEntry(id, classOfEvent, eventTypeVersion, entryData, entryVersion, metadata);
        }
    }
}