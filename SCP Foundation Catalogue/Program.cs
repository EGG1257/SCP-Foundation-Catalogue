using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace SCP_Foundation_Catalogue
{
    public enum ObjectClass
    {
        Safe = 1,
        Euclid = 2,
        Keter = 3,
        Thaumiel = 4,
        Apollyon = 5,
        Archon = 6,
        Cernunnos = 7,
        Ticonderoga = 8,
        Explained = 9,
        Neutralized = 10,
        Decommissioned = 11,
        Pending = 12,
        Uncontained = 13,

    }
    internal partial class Program
    {
        static SCPRegistry registry = new SCPRegistry();
        private static void Main(string[] args)
        {
            string argument = "";//for pulling the id from the get command
            string userInput;//for storing the inputs by the user
            Console.WriteLine("For a list of available system commands, return 'help'.");
            
            while (true)
            {
                argument = "";
                Console.Write("> ");
                userInput = Console.ReadLine();

                int spaceIndex = userInput.IndexOf(' ');
                string command = spaceIndex != -1 ? userInput.Substring(0, spaceIndex) : userInput;
                if (spaceIndex != -1)
                    argument = userInput.Substring(spaceIndex + 1);

                switch (command)
                {
                    case "help":
                        Help();
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "list":
                        ListAllSCPs(); 
                        break;
                    case "get":
                        if (argument == "")
                            Console.WriteLine("ERROR: Missing argument. Usage: get <id>");
                        else
                            SearchSCP(argument);
                        break;
                    case "add":
                        AddSCP();
                        break;

                }

            }
        }

        static void Help()
        {
            Console.WriteLine("List of available commands:\n");
            Console.WriteLine("'help' ---------- Show information on available commands");
            Console.WriteLine("'clear' --------- Clear the console");
            Console.WriteLine("'list' ---------- List all stored folders");
            Console.WriteLine("'get <id>' ------ View the files of a specified entry");
            Console.WriteLine("'add' ----------- Create a new entry\n");

        }
    }
}