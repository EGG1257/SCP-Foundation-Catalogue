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

        static void ListAllSCPs(string argument)//list all the scp's
        {
            if (string.IsNullOrEmpty(argument))
            {
                Console.WriteLine("\n=== All Registered SCPs ===");//fix styling

                foreach (SCPEntry entry in registry.All)
                    entry.PrintInfo(detailed: false);
            }
            else
            {
                int min = 0;
                int max = 0;
                string[] parts = argument.Split(' ');
                if (parts.Length == 1)
                {
                    max = 10000;
                    if (!int.TryParse(parts[0], out min))
                        Console.WriteLine($"ERROR: Invalid argument. Usage: ls [minimum value] [maximum value]");
                    else
                        Range(min, max);
                }
                else
                {
                    if (!int.TryParse(parts[0], out min))
                        Console.WriteLine($"ERROR: Invalid argument. Usage: ls [minimum value] [maximum value]");
                    else if (!int.TryParse(parts[1], out max))
                        Console.WriteLine($"ERROR: Invalid argument. Usage: ls [minimum value] [maximum value]");
                    else
                        Range(min, max);
                }


            }
            

        }

        static void Range(int min, int max)
        {
            int count = 0;
            foreach (SCPEntry entry in registry.All)
            {
                // pull the number out of the ID e.g. "SCP-006" -> 6
                string numPart = entry.Id.Replace("SCP-", "").Split('-')[0]; // handles SCP-096-A too
                if (int.TryParse(numPart, out int scpNumber) && scpNumber >= min && scpNumber <= max)
                {
                    entry.PrintInfo(detailed: false);
                    count++;
                }
            }

            if (count == 0)
                Console.WriteLine($"  No entries found in range {min}-{max}.");
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
