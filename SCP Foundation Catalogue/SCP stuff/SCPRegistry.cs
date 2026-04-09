using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    class SCPRegistry
    {
        private readonly Dictionary<string, SCPEntry> _entries = new();

        public void Add(SCPEntry entry)
        {
            if (_entries.ContainsKey(entry.Id))
                throw new ArgumentException($"{entry.Id} is already registered.");
            _entries[entry.Id] = entry;
        }

        public SCPEntry Get(string id) => _entries.TryGetValue(id.ToUpper(), out var entry) ? entry : null;

        public IEnumerable<SCPEntry> All => _entries.Values;
    }
}