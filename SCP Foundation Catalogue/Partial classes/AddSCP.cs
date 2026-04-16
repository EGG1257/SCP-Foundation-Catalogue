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
            Console.WriteLine("What is the object class of the SCP:");
            Console.WriteLine("[1] Safe           [5] Apollyon       [9]  Explained       [13] Uncontained\n[2] Euclid         [6] Archon         [10] Neutralized\n[3] Keter          [7] Cernunnos      [11] Decommissioned\n[4] Thaumiel       [8] Ticonderoga    [12] Pending");
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
            objectClass = (ObjectClass)intInput;

            //strings for storing confainment details and description
            string containment = "";
            string description = "";
            List<string> additionalFiles = new List<string>();
            Dictionary<string, string> addendums = new Dictionary<string, string>(); //dictionary because there can be multiple addendums with names

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

            //for adding any number of addendums
            bool addeBool = true;
            while (addeBool)
            {
                string addeName;
                string addeText;
                Console.Write("Would you like to add an addendum (y/n)\n> ");//ask if user wants to add addendum
                string adenYn = Console.ReadLine()?.Trim().ToLower();

                if (adenYn == "y")//if yes ask for name and inards and then write those to the addendum dictionary
                {
                    Console.Write("What is the name of the addendum\n> ");
                    addeName = Console.ReadLine();
                    addeText = ReadLocation("What is the addendum");
                    addendums.Add(addeName, addeText);//add to dictioanry
                }
                else if (adenYn == "n") addeBool = false;//if no break addendum loop
                else Console.WriteLine("Invalid input.");
            }

            //for adding aditional files
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
            newSCP.Addendums = addendums;
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
