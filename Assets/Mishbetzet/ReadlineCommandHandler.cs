using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mishbetzet
{
    /// <summary>
    /// Class whose job is to get the input from the console and to invoke executaion of the passed command
    /// </summary>
    internal class ReadlineCommandHandler
    {
        /// <summary>
        /// A dictionary which contains the client commands, the class can't exist without them.
        /// Must get it from the core when initialized.
        /// </summary>
        private Dictionary<string, Command> dictionary;

        public ReadlineCommandHandler(Dictionary<string, Command> dictionary)
        {
            this.dictionary = dictionary;
            var ConsoleInputGetter = new Thread(WaitForInput);
            ConsoleInputGetter.Start();
        }
        private void WaitForInput()
        {
            while (true)
            {
                OnReadLineCommand(Console.ReadLine());
            }
        }
        private void OnReadLineCommand(string input)
        {
            string[] s = input.Split(" ");
            if (dictionary.TryGetValue(s[0], out Command command))
                command.Execute(s);
        }
    }
}
