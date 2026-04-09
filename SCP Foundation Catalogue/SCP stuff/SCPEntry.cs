namespace SCP_Foundation_Catalogue
{
    abstract class SCPEntry
    {
        protected string id;
        protected string name;
        protected ObjectClass objectClass;

        public string Id => id;
        public string Name => name;
        public ObjectClass ObjectClass => objectClass;

        public SCPEntry(string id, string name, ObjectClass objectClass)
        {
            this.id = id;
            this.name = name;
            this.objectClass = objectClass;
        }

        public abstract void PrintInfo(bool detailed = false);

        
    }
}