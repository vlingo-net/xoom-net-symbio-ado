using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace Vlingo.Symbio.Ado.Common.SqlServer
{
    public class SqlEntryAdapter<TEntry> where TEntry : IEntry
    {
        private readonly ICollection<DbColumn> dbColumns;
        private readonly IDictionary<int, MethodInfo> propertySetters = new Dictionary<int, MethodInfo>();

        public SqlEntryAdapter(ICollection<DbColumn> dbColumns)
        {
            this.dbColumns = dbColumns;
            var type = typeof(TEntry);
            foreach (var column in dbColumns)
            {
                if (!column.ColumnOrdinal.HasValue) continue;
                var setter = type.GetProperty(column.ColumnName, BindingFlags.IgnoreCase)?.SetMethod;
                if (setter is null) continue;
                propertySetters.Add(column.ColumnOrdinal.Value, setter);
            }
        }

        public TEntry CreateEntry(object[] fieldValues)
        {
            var instance = Activator.CreateInstance<TEntry>();
            foreach (var column in dbColumns)
            {
                if (propertySetters.TryGetValue(column.ColumnOrdinal ?? -1, out var setter))
                {
                    setter.Invoke(instance, new[] {fieldValues[column.ColumnOrdinal!.Value]});
                }
            }

            return instance;
        }
    }
}