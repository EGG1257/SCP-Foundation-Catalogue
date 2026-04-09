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
    }
}
