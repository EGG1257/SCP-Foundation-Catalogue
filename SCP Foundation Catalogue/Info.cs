using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    internal class Info
    {
        private bool menu = true;

        public void Start()
        {
            int intInput;
            while (menu)
            {
                Console.Clear();
                Console.WriteLine("Information terminal:");
                Console.WriteLine("[1] The SCP foundation");
                Console.WriteLine("[2] Object classes");
                Console.Write("[3] Exit\n>");

                string input = Console.ReadLine();
                if (int.TryParse(input, out intInput))
                    Selection(intInput);
                else
                    Console.WriteLine("Invalid input.");
                
            }
        }

        private void Selection(int input)
        {
            switch(input)
            {
                case 1:
                    SCPFoundation();
                    break;
                case 2:
                    ObjectClasses();
                    break;
                case 3:
                    menu = false; 
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;


            }
        }

        private void SCPFoundation()
        {
            Program.ClearConsole();
            Console.WriteLine("  ┌─The SCP Foundation:");
            PrintBlock("  │  ", "Operating clandestine and worldwide, the Foundation acts beyond conventional jurisdiction, with the task of containing anomalous objects, entities, and phenomena.\r\n\r\nWe maintain an extensive database of information regarding anomalies requiring Special Containment Procedures, commonly referred to as \"SCPs\"; all of which undermine the natural laws that the people of the world implicitly trust in.\r\n\r\nWe operate to maintain normalcy, so that the worldwide civilian population can live and go on with their daily lives without fear, mistrust, or doubt in their personal beliefs, and to maintain human independence from extraterrestrial, extradimensional, and other extranormal influence.\r\n\r\nOur mission is threefold:\r\n\r\n• Secure — secure anomalies to prevent them from falling into the hands of civilian or rival agencies through extensive observation and surveillance, acting to intercept anomalies at the earliest opportunity.\r\n• Contain — contain anomalies to prevent spread of their influence or effects; by either relocating, concealing, or dismantling them, or by suppressing public dissemination of knowledge thereof.\r\n• Protect — protects humanity as well as the anomalies themselves until such time that they are either fully understood or new theories of science can be devised based on their properties and behavior.\r\nAdditional information will have been provided upon your joining us in pursuit of our goals. Welcome to the Foundation, and good luck.\n");
            Console.WriteLine("  └─");
            Console.WriteLine("[Press ENTER to return]");
            Console.ReadLine();
            return;
        }

        private void ObjectClasses()
        {
            Program.ClearConsole();
            Console.WriteLine("  ┌─Object Classes:");
            PrintBlock("  │  ", "All anomalous objects, entities, and phenomena requiring Special Containment Procedures are assigned an Object Class. An Object Class is a part of the standard SCP template and serves as a rough indicator for how difficult an object is to contain. In universe, Object Classes are for the purposes of identifying containment needs, research priority, budgeting, and other considerations. An SCP's Object Class is determined by a number of factors, but the most important factors are the difficulty and the purpose of its containment.\n");
            Console.WriteLine("  │  ┌─Primary Classes:");
            PrintBlock("  │  │  ", "These are the most common Object Classes used in SCP articles, and make up the bulk of the objects.\n");
            Console.WriteLine("  │  │  ┌─Safe:");
            PrintBlock("  │  │  │  ", "Safe-class SCP objects are anomalies that are easily and safely contained. This is often due to the fact that the Foundation has researched the SCP well enough that containment does not require significant resources or that the anomalies require a specific and conscious activation or trigger. Classifying an SCP as Safe, however, does not mean that handling or activating it does not pose a threat.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Euclid:");
            PrintBlock("  │  │  │  ", "Euclid-class SCP objects are anomalies that require more resources to contain completely or where containment isn't always reliable. Usually this is because the SCP is insufficiently understood or inherently unpredictable. Euclid is the Object Class with the greatest scope, and it's usually a safe bet that an SCP will be this class if it doesn't easily fall into any of the other standard Object Classes.\r\n\r\nAs a note, any SCP that's autonomous or sapient is generally classified as Euclid, due to the inherent unpredictability of an object that can act or think on its own.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Keter:");
            PrintBlock("  │  │  │  ", "Keter-class SCP objects are anomalies that are exceedingly difficult to contain consistently or reliably, with containment procedures often being extensive and complex. The Foundation often can't contain these SCPs well due to not having a solid understanding of the anomaly, or lacking the technology to properly contain or counter it. A Keter SCP does not mean the SCP is dangerous, just that it is simply very difficult or costly to contain.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Thaumiel:");
            PrintBlock("  │  │  │  ", "Thaumiel-class SCP objects are anomalies that the Foundation specifically uses to contain other SCPs. Even the mere existence of Thaumiel-class objects is classified at the highest levels of the Foundation, and their locations, functions, and current status are known to few Foundation personnel outside of the O5 Council.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  └─");
            Console.WriteLine("  │  ┌─Secondary Classes:");
            PrintBlock("  │  │  ", "The following object classes are less frequently used, and designate anomalies with unusual containment requirements. These secondary classes may also be described as esoteric classes, in addition to many other esoteric classes used even less frequently.\n");
            Console.WriteLine("  │  │  ┌─Apollyon:");
            PrintBlock("  │  │  │  ", "Apollyon-class SCP objects are anomalies that cannot be contained, are expected to breach containment imminently, or some other similar scenario. Such anomalies are usually associated with world-ending threats or a K-Class Scenario of some kind, and require a massive effort from the Foundation to deal with.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Archon:");
            PrintBlock("  │  │  │  ", "Archon-class SCP objects are anomalies that could theoretically be contained but are best left uncontained for some reason. Archon SCPs may be a part of consensus reality that is difficult to fully contain, or may have adverse effects if put into containment. These SCPs are not uncontainable - the defining feature of the class is that the Foundation chooses to not put the anomaly into containment.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Cernunnos:");
            PrintBlock("  │  │  │  ", "Cernunnos-class SCP objects could be functionally contained, but for logistical or ethical reasons the Foundation has chosen not to at this time. The anomaly may still be partially contained or actively concealed from the public, but the costs of fully containing it are deemed to outweigh the benefits.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Ticonderoga:");
            PrintBlock("  │  │  │  ", "Ticonderoga-class SCP objects cannot be contained, but also do not need to be contained. While these anomalies are similar to Archon-class SCPs in that their containment is unnecessary, Ticonderoga-class SCPs are distinguished by also not being containable given the Foundation's current knowledge and resources. This may be due to their widespread or ubiquitous occurrence on Earth, despite the general public remaining unaware of their existence or anomalous nature, although the Ticonderoga class also includes extraterrestrial anomalies beyond the current reach of the Foundation.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  └─");
            Console.WriteLine("  │  ┌─Non-Standard Object Classes:");
            PrintBlock("  │  │  ", "Most of the following Object Classes indicate that an SCP has not yet been assigned a standard object class, or that it was previously assigned a Standard Object Class but no longer requires containment.\n");
            Console.WriteLine("  │  │  ┌─Explained:");
            PrintBlock("  │  │  │  ", "Explained SCP objects were formerly classified as anomalies before being completely and fully understood, to the point where their effects are now explainable by mainstream science, or were falsely identified as an anomaly before being debunked as a hoax or misunderstanding. While most of these articles are listed separately as Explained SCPs, a mainlist SCP may be reclassified as Explained after further investigation into the apparent anomaly.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Neutralized:");
            PrintBlock("  │  │  │  ", "Neutralized SCP objects were classified as anomalies but are no longer anomalous, due to the object being intentionally or accidentally destroyed or disabled, or simply due to its anomalous properties or effects ceasing.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Decommissioned:");
            PrintBlock("  │  │  │  ", "Decommissioned SCP objects are anomalies that have been intentionally destroyed or stripped of their anomalous properties by the SCP Foundation. As the Foundation usually tries to contain rather than neutralize anomalous objects, this object class is only used when it is not possible to fully contain an anomaly, or when excessive expenditure of resources is required to keep an anomaly in containment. Decommissioning may also be performed for ethical reasons, particularly when containment necessitates extreme suffering or is unable to prevent excessive loss of life. To avoid any unnecessary losses, decommissioning anomalies requires authorization from a higher authority, such as the O5 Council, the Ethics Committee, or the Decommissioning Department.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Pending:");
            PrintBlock("  │  │  │  ", "SCP objects that have not yet been assigned an object class may be labelled as Pending. This is used to indicate that the Foundation does not currently have enough information to assign an object class to the anomaly.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  │  ┌─Uncontained:");
            PrintBlock("  │  │  │  ", "SCP objects that are not yet contained may be assigned an object class, often Keter, but in some articles Uncontained is used in place of an object class to emphasize that ongoing effort is required to establish or restore containment.");
            Console.WriteLine("  │  │  └─");
            Console.WriteLine("  │  └─");
            Console.WriteLine("  └─");
            Console.WriteLine("[Press ENTER to return]");
            Console.ReadLine();
            return;
        }

        private void PrintBlock(string prefix, string text, int maxWidth = 100)
        {
            int contentWidth = maxWidth - prefix.Length;

            var lines = text.Split('\n');

            foreach (var rawLine in lines)
            {
                string line = rawLine.TrimEnd();

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


    }
}
