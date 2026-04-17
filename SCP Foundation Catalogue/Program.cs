using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Diagnostics;

namespace SCP_Foundation_Catalogue
{
    public enum ObjectClass//the different object classes an scp can have
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
            Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..")));
            Console.Write("\x1b[38;2;250;209;153m");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            registry.LoadAll(); //Load previous entries
            BootSequence();//play the boot sequence in BootStuff.cs
            PrintStartup();//print the startup info
            string argument = "";//for pulling the id from the get command
            string userInput;//for storing the inputs by the user
            Console.WriteLine("For a list of available system commands, return 'help'.");
            
            while (true)
            {
                argument = "";
                Console.Write("H:\\> ");
                userInput = Console.ReadLine();

                //Cut the code in half to seperate the command and argument
                int spaceIndex = userInput.IndexOf(' ');
                string command = spaceIndex != -1 ? userInput.Substring(0, spaceIndex) : userInput;
                if (spaceIndex != -1)
                    argument = userInput.Substring(spaceIndex + 1).ToUpper();

                switch (command)//All available commands will do what they are programmed to do when input
                {
                    case "help":
                        Help();//show help menu
                        break;
                    case "cls":
                        ClearConsole();//clear the console
                        break;
                    case "ls":
                        ListAllSCPs(argument);
                        break;
                    case "get":
                        if (argument == "")//if an argument is not provided return an error
                            Console.WriteLine("ERROR: Missing argument. Usage: get <id>");
                        else
                            SearchSCP(argument);//show the details of the selected scp
                        break;
                    case "open":
                        if (argument == "")//if an argument is not provided return an error
                            Console.WriteLine("ERROR: Missing argument. Usage: open <file path>");
                        else
                            OpenFile(argument);
                        break;
                    case "add":
                        AddSCP();//run the program for adding a new scp
                        break;
                    case "rm":
                        if (argument == "")//if an argument is not provided return an error
                            Console.WriteLine("ERROR: Missing argument. Usage: rm <id>");
                        else
                            DeleteSCP(argument);
                        break;
                    case "info":
                        Info info = new Info();
                        info.Start();//launch the info terminal
                        break;
                    default:
                        if (userInput == "")
                            break;
                        Console.WriteLine($"{userInput}: command not found");
                        break;
                }

            }
        }

        static void Help()//list of available commands
        {
            Console.WriteLine("List of available commands:\n");
            Console.WriteLine("'help' --------------- Show information on available commands");
            Console.WriteLine("'cls' ---------------- Clear the console");
            Console.WriteLine("'ls [min] [max]' ----- List stored entries");
            Console.WriteLine("'get <id>' ----------- View the files of a specified entry");
            Console.WriteLine("'add' ---------------- Create a new entry");
            Console.WriteLine("'open <file>' -------- Open a file");
            Console.WriteLine("'info' --------------- Access the information terminal");
            Console.WriteLine("'rm <id>' ------------ Delete an entry\n");
            Console.WriteLine("* <> is a required argument. [] is an optional argument\n");

        }


        static void PrintStartup()//startup info that displays when the program starts
        {
            Console.WriteLine($"System information as of {DateTime.Now:ddd MMM dd HH:mm:ss} UTC {DateTime.Now.Year}\n");
            Console.WriteLine($"{"Load:",-20} {"4.49",-20} {"Processes:",-25} 117");
            Console.WriteLine($"{"Usage of H:\\:",-20} {"2% of 39.48GB",-20} {"Users logged in:",-25} 1");
            Console.WriteLine($"{"Memory usage:",-20} {"40%",-20} {"Site:",-25} Site-19");
            Console.WriteLine($"{"Status:",-20} {"SECURE",-20} {"Clearance:",-25} Level 3");
            Console.WriteLine();
        }

        static void OpenFile(string filePath)//Used for opening file using that file types default viewer
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }

        public static void ClearConsole()
        {
            Console.Clear();
            Console.Write("\x1b[3J"); // ANSI escape code to clear the scrollback buffer
        }

    }
}