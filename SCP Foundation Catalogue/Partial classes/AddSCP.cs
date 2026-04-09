using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    internal partial class Program //Made it a partial class so that this giant chunk of code can be stored in a seperate file
    {
        static void AddSCP()
        {
            int intInput = 0;
            bool selected = true;
            //Ask for SCP ID
            Console.Write("What is the id of the SCP (e.g. SCP-1234)\n> ");
            string id = Console.ReadLine().ToUpper();
            if (registry.Get(id) != null) { Console.WriteLine($"{id} already exists."); return; }
            //Ask for SCP Name
            Console.Write("What is the name of the SCP\n> ");
            string name = Console.ReadLine();
            //Ask for SCP Object class
            Console.WriteLine("What is the object class of the SCP:\n[1] Safe\n[2] Euclid\n[3] Keter\n[4] Thaumiel\n[5] Show secondary classes\n[6] Show non-standard object classes");
            ObjectClass objectClass;
            while (selected)//Input and validate the selection
            {
                Console.Write(">");
                string input = Console.ReadLine();

                if (int.TryParse(input, out intInput))
                    selected = false;
                else
                    Console.WriteLine("Invalid input.");
            }
            selected = true;

            if (intInput == 5)//Output the secondary classes and ask for input again
            {
                Console.WriteLine("[1] Apollyon\n[2] Archon\n[3] Cernunnos\n[4] Ticonderoga");

                while (selected)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out intInput))
                        selected = false;
                    else
                        Console.WriteLine("Invalid input.");

                    intInput += 4;//add 4 to align with the object class enum
                }
            }
            else if (intInput == 6)//Output the non-standard classes and ask for input again
            {
                Console.WriteLine("[1] Explained\n[2] Neutralized\n[3] Decommissioned\n[4] Pending\n[5] Uncontained");

                while (selected)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out intInput))
                        selected = false;
                    else
                        Console.WriteLine("Invalid input.");

                    intInput += 8;//add 8 to align with the object class enum
                }
            }
            objectClass = (ObjectClass)intInput;

            string containment = "";
            string description = "";
            Console.Write("Would you like to add a special containment procedure (y/n)");
            while (true)
            {
                Console.Write("\n> ");
                string yn = Console.ReadLine();
                if (yn == "y" || yn == "Y")
                {
                    containment = ReadFromFileOrConsole("What are the special containment procedures");
                    break;
                }
                else if (yn == "n" || yn == "N")
                    break;
                else
                    Console.WriteLine("Invalid input.");

            }
            
            Console.Write("Would you like to add a description (y/n)");
            while (true)
            {
                Console.Write("\n> ");
                string yn = Console.ReadLine();
                if (yn == "y" || yn == "Y")
                {
                    description = ReadFromFileOrConsole("What is the description");
                    break;
                }
                else if (yn == "n" || yn == "N")
                    break;
                else
                    Console.WriteLine("Invalid input.");
            }

            registry.Add(new SCP(id, name, objectClass, containment, description));
            Console.WriteLine($"{id} added successfully.");
        }

        static string ReadFromFileOrConsole(string prompt)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("[1] Type/paste a single line");
            Console.WriteLine("[2] Load from a .txt file");
            Console.Write("> ");

            string choice = Console.ReadLine()?.Trim();

            if (choice == "2")
            {
                Console.Write("Enter the full file path (e.g. C:\\Users\\You\\containment.txt): ");
                string path = Console.ReadLine()?.Trim().Trim('"'); // strip quotes if dragged in

                if (File.Exists(path))
                    return File.ReadAllText(path);
                else
                {
                    Console.WriteLine("File not found, falling back to manual input.");
                }
            }

            return Console.ReadLine();
        }
    }
}
