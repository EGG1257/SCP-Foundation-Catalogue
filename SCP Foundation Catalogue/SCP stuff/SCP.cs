using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    class SCP : SCPEntry
    {
        public string Description { get; set; }
        public string ContainmentProcedures { get; set; }

        public List<string> AdditionalFiles { get; set; } = new List<string>();

        public SCP(string id, string name, ObjectClass objectClass, string containmentProcedures, string description) : base(id, name, objectClass)
        {
            ContainmentProcedures = containmentProcedures;
            Description = description;
            
        }

        public override void PrintInfo(bool detailed = false)//print the info of an scp
        {
            if (detailed)
                ClearConsole();
            Console.WriteLine($"\n  ┌─ {id}: {name}");
            Console.WriteLine($"  │  Object Class: {objectClass}");

            if (detailed)//print the rest of the info if detailed is true
            {
                Console.WriteLine($"  │");
                if (ContainmentProcedures != "")
                {
                    Console.WriteLine($"  │  ┌─Special Containment Procedures:");
                    PrintBlock(ContainmentProcedures);
                    Console.WriteLine("  │  └─");
                }

                if (Description != "")
                {
                    Console.WriteLine($"  │");
                    Console.WriteLine($"  │  ┌─Description:");
                    PrintBlock(Description);
                    Console.WriteLine("  │  └─");
                }

                if (AdditionalFiles.Count > 0)
                {
                    Console.WriteLine($"  │");
                    Console.WriteLine($"  │  ┌─ Additional Files:");
                    foreach (string file in AdditionalFiles)
                        Console.WriteLine($"  │  │  {Path.GetFileName(file)}   →   {file}");
                    Console.WriteLine($"  │  └─");
                }
            }

            Console.WriteLine("  └─");
        }

        //This is code I found online to allow the large paragraphs to print in a clean fasion that is easy to read.
        private void PrintBlock(string text, int maxWidth = 100)
        {
            string prefix = "  │  │  ";
            int contentWidth = maxWidth - prefix.Length;

            var lines = text.Split('\n');

            foreach (var rawLine in lines)
            {
                string line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine(prefix);
                    continue;
                }

                string indent = "";
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && int.TryParse(line.Substring(0, colonIndex), out _))
                {
                    indent = new string(' ', colonIndex + 2);
                }
                else if (line.StartsWith("•"))
                {
                    indent = new string(' ', 2); // indent wrapped lines to align with text after the bullet
                }

                var words = line.Split(' ');
                string currentLine = "";
                bool firstLine = true;

                foreach (var word in words)
                {
                    string testLine = currentLine + word + " ";

                    if (testLine.Length > contentWidth)
                    {
                        Console.WriteLine(prefix + currentLine.TrimEnd());
                        currentLine = firstLine ? indent + word + " " : indent + word + " ";
                        firstLine = false;
                    }
                    else
                    {
                        currentLine += word + " ";
                    }
                }

                if (currentLine.Length > 0)
                {
                    Console.WriteLine(prefix + currentLine.TrimEnd());
                }
            }
        }

        static void ClearConsole()//clears the console and the scrollback history so that it fully clears the console and not just the visible stuff.
        {
            Console.Clear();
            Console.Write("\x1b[3J"); // ANSI escape code to clear the scrollback buffer
        }
    }
}