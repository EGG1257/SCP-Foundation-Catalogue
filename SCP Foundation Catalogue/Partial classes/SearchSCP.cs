using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    internal partial class Program
    {
        static void SearchSCP(string id)
        {
            SCPEntry entry = registry.Get(id);

            if (entry == null)
            {
                Console.WriteLine($"{id} was not found in the registry.");
                return;
            }

            entry.PrintInfo(detailed: true);
        }

        static void ListAllSCPs()
        {
            Console.WriteLine("\n=== All Registered SCPs ===");

            foreach (SCPEntry entry in registry.All)
                entry.PrintInfo(detailed: false);

        }

        static void DeleteSCP(string id)
        {
            id = id.ToUpper().Trim();
            SCPEntry entry = registry.Get(id);

            if (entry == null)
            {
                Console.WriteLine($"ERR_NOT_FOUND: {id} does not exist in the registry.");
                return;
            }

            // Confirm before deleting
            Console.Write($"Are you sure you want to delete {id}: {entry.Name}? This cannot be undone. (y/n)\n> ");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm != "y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            // Remove from registry
            registry.Remove(id);

            // Delete the folder and everything in it
            string folderPath = Path.Combine("SCPDatabase", id);
            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, recursive: true); // recursive deletes all files inside too

            Console.WriteLine($"SUCCESS: {id} has been deleted.");
        }
    }
}
