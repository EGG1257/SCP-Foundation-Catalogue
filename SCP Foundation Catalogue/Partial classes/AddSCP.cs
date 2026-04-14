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
            Console.Write("What is the id of the SCP (e.g. SCP-123)\n> ");
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

            //strings for storing confainment details and description
            string containment = "";
            string description = "";
            List<string> additionalFiles = new List<string>();
            //give the option to add a containment procedure
            Console.Write("Would you like to add a special containment procedure (y/n)");
            while (true)
            {
                Console.Write("\n> ");
                string conYn = Console.ReadLine()?.Trim().ToLower();
                if (conYn == "y")
                {
                    containment = ReadLocation("What are the special containment procedures");
                    break;
                }
                else if (conYn == "n")
                    break;
                else
                    Console.WriteLine("Invalid input.");

            }
            
            //give the option to add a description
            Console.Write("Would you like to add a description (y/n)");
            while (true)
            {
                Console.Write("\n> ");
                string descYn = Console.ReadLine()?.Trim().ToLower();
                if (descYn == "y")
                {
                    description = ReadLocation("What is the description");
                    break;
                }
                else if (descYn == "n")
                    break;
                else
                    Console.WriteLine("Invalid input.");
            }


            Console.Write("Would you like to add any additional files (y/n)\n> ");
            string addYn = Console.ReadLine()?.Trim().ToLower();

            if (addYn == "y")
            {
                string scpFolder = Path.Combine("SCPDatabase", id);
                Directory.CreateDirectory(scpFolder);

                while (true)
                {
                    Console.Write("Enter file path to attach (or 'done' to finish)\n> ");
                    string filePath = Console.ReadLine()?.Trim().Trim('"');

                    if (filePath.ToLower() == "done") 
                        break;

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"ERROR: No file found at '{filePath}'");
                        continue;
                    }

                    string fileName = Path.GetFileName(filePath);
                    string destination = Path.Combine(scpFolder, fileName);

                    File.Copy(filePath, destination, overwrite: true);
                    additionalFiles.Add(destination);
                    Console.WriteLine($"SUCCESS: {fileName} attached.");
                }
            }

            SCP newSCP = new SCP(id, name, objectClass, containment, description);//create the new SCP
            newSCP.AdditionalFiles = additionalFiles;
            registry.Add(newSCP);//add it to the registry
            registry.Save(newSCP);//save it to the json file
            Console.WriteLine($"{id} added successfully.");
        }

        static string ReadLocation(string prompt)//for reading a file to allow for bigger and more complex text to be added
        {
            Console.WriteLine(prompt);
            Console.WriteLine("[1] Type a single line");
            Console.WriteLine("[2] Load from a .txt file (for large block of text)\n");
            Console.Write("> ");

            string choice = Console.ReadLine()?.Trim();

            if (choice == "2")
            {
                Console.Write("Enter the full file path (e.g. C:\\Users\\person\\containment.txt): ");
                string path = Console.ReadLine()?.Trim().Trim('"'); // strip quotes if dragged in

                if (File.Exists(path))
                    return File.ReadAllText(path);
                else
                {
                    //return to manual input if file not found
                    Console.WriteLine("File not found, falling back to manual input.");
                }
            }

            return Console.ReadLine();
        }
    }
}
