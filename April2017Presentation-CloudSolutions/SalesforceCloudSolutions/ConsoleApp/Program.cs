using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuCommands = new IMenuCommand[]
            {
                new SimpleQueryCommand (),
                new ExitCommand()
            };

            while (true)
            {
                Console.WriteLine(value: "Salesforce Cloud Solutions");
                Console.WriteLine(value: "What do you want to do?");

                int i = 1;
                foreach (var menuCommand in menuCommands)
                {
                    Console.WriteLine(format: "{0}. {1}", arg0: i++, arg1: menuCommand.Description);
                }

                //Read until the input is valid
                string userChoice = string.Empty;
                int commandIndex = -1;

                do
                {
                    userChoice = Console.ReadLine();
                }

                while (!int.TryParse(userChoice, out commandIndex) || commandIndex > menuCommands.Length);

                // Execute the command.
                menuCommands[commandIndex - 1].Execute();
            }

        }
    }
}
