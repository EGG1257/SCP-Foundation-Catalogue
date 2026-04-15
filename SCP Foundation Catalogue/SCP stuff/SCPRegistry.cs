using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    class SCPRegistry
    {
        static string dbPath = "SCPDatabase";
        private readonly Dictionary<string, SCPEntry> _entries = new();

        public void Save(SCPEntry entry)//Save the SCPs to their own json file
        {
            string folderPath = Path.Combine(dbPath, entry.Id);
            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, $"{entry.Id}.json");

            SCP scp = (SCP)entry;

            string filesJson = string.Join(", ", scp.AdditionalFiles.Select(f => $"\"{f.Replace("\\", "\\\\")}\""));

            string json = $@"{{
            ""id"": ""{scp.Id}"",
            ""name"": ""{scp.Name}"",
            ""objectClass"": ""{scp.ObjectClass}"",
            ""containmentProcedures"": {JsonEscape(scp.ContainmentProcedures)},
            ""description"": {JsonEscape(scp.Description)},
            ""additionalFiles"": [{filesJson}]
            }}";

            File.WriteAllText(filePath, json);
        }

        public void LoadAll()//used for loading the SCPs to the registry when the program starts
        {
            if (!Directory.Exists(dbPath)) return;

            foreach (string folder in Directory.GetDirectories(dbPath))
            {
                string id = Path.GetFileName(folder);
                string filePath = Path.Combine(folder, $"{id}.json");
                if (!File.Exists(filePath)) continue;

                string json = File.ReadAllText(filePath);

                string entryId = ParseField(json, "id");
                string name = ParseField(json, "name");
                string objectClassStr = ParseField(json, "objectClass");
                string containment = ParseField(json, "containmentProcedures");
                string description = ParseField(json, "description");
                List<string> files = ParseArray(json, "additionalFiles");

                if (Enum.TryParse(objectClassStr, out ObjectClass objectClass))
                {
                    SCP scp = new SCP(entryId, name, objectClass, containment, description);
                    scp.AdditionalFiles = files;
                    Add(scp);
                }
            }
        }

        public void Add(SCPEntry entry)
        {
            if (_entries.ContainsKey(entry.Id))
                throw new ArgumentException($"{entry.Id} is already registered.");
            _entries[entry.Id] = entry;
        }

        public void Remove(string id)
        {
            _entries.Remove(id.ToUpper());
        }

        private string ParseField(string json, string key)
        {
            string search = $"\"{key}\": \"";
            int start = json.IndexOf(search);
            if (start == -1) return "";
            start += search.Length;

            //walk forward until we find an unescaped closing quote
            int end = start;
            while (end < json.Length)
            {
                if (json[end] == '\\')
                {
                    end += 2; //skip escaped character e.g. \" or \n
                    continue;
                }
                if (json[end] == '"')
                    break;
                end++;
            }

            string value = json.Substring(start, end - start);

            //convert escape sequences back to real characters
            value = value.Replace("\\n", "\n")
                         .Replace("\\\"", "\"")
                         .Replace("\\\\", "\\");

            return value;
        }

        private string JsonEscape(string value)
        {
            value = value.Replace("\\", "\\\\")
                         .Replace("\"", "\\\"")
                         .Replace("\r\n", "\\n")
                         .Replace("\n", "\\n");
            return $"\"{value}\"";
        }

        private List<string> ParseArray(string json, string key)
        {
            List<string> results = new List<string>();
            string search = $"\"{key}\": [";
            int start = json.IndexOf(search);
            if (start == -1) return results;

            start += search.Length;
            int end = json.IndexOf("]", start);
            string array = json.Substring(start, end - start);

            foreach (string part in array.Split(','))
            {
                string value = part.Trim().Trim('"').Replace("\\\\", "\\");
                if (!string.IsNullOrEmpty(value))
                    results.Add(value);
            }

            return results;
        }

        public SCPEntry Get(string id) => _entries.TryGetValue(id.ToUpper(), out var entry) ? entry : null;

        public IEnumerable<SCPEntry> All => _entries.Values;
    }
}