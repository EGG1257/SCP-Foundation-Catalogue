namespace SCP_Foundation_Catalogue
{
    abstract class SCPEntry
    {
        protected string id;
        protected string name;
        protected ObjectClass objectClass;
        protected List<SCPEntry> subEntries;

        // Back-reference so a child knows its parent
        public SCPEntry Parent { get; private set; }

        public string Id => id;
        public string Name => name;
        public ObjectClass ObjectClass => objectClass;
        public IReadOnlyList<SCPEntry> SubEntries => subEntries.AsReadOnly();

        public SCPEntry(string id, string name, ObjectClass objectClass)
        {
            this.id = id;
            this.name = name;
            this.objectClass = objectClass;
            this.subEntries = new List<SCPEntry>();
        }

        public void AddSubEntry(SCPEntry entry)
        {
            if (entry == this)
                throw new ArgumentException("An SCP cannot be a sub-entry of itself.");
            if (subEntries.Contains(entry))
                throw new ArgumentException($"{entry.id} is already a sub-entry of {id}.");

            subEntries.Add(entry);
            entry.Parent = this;   // wire up the back-reference
        }

        // Every concrete subclass decides how to display itself
        public abstract void PrintInfo(bool detailed = false);

        // Shared formatting helper for the relationship lines
        protected void PrintRelationships()
        {
            if (Parent != null)
                Console.WriteLine($"  │  Parent     : {Parent.id} ({Parent.name})");

            if (subEntries.Count > 0)
            {
                Console.Write("  │  Sub-entries: ");
                Console.WriteLine(string.Join(", ", subEntries.ConvertAll(e => e.id)));
            }
        }
    }
}