using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    internal partial class Program
    {
        static void SearchSCP(string id)//print the info of a specified scp
        {
            SCPEntry entry = registry.Get(id);

            if (entry == null)
            {
                Console.WriteLine($"{id} was not found in the registry.");
                return;
            }

            entry.PrintInfo(detailed: true);//add the extra details
        }

        static void ListAllSCPs()//list all the scp's
        {
            Console.WriteLine("\n=== All Registered SCPs ===");

            foreach (SCPEntry entry in registry.All)
                entry.PrintInfo(detailed: false);

        }

        static void DeleteSCP(string id)//delete a selected scp
        {
            id = id.ToUpper().Trim();
            SCPEntry entry = registry.Get(id);

            if (entry == null)
            {
                Console.WriteLine($"ERR_NOT_FOUND: {id} does not exist in the registry.");
                return;
            }

            //Ask before deleting
            Console.Write($"Are you sure you want to delete {id}: {entry.Name}? This cannot be undone. (y/n)\n> ");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm != "y")//cancel if y not input
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            //remove from the registry
            registry.Remove(id);

            //delete the folder and all it's innards
            string folderPath = Path.Combine("SCPDatabase", id);
            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, recursive: true);

            Console.WriteLine($"SUCCESS: {id} has been deleted.");
        }
    }
}
