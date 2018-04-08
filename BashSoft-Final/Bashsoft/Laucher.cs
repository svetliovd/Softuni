using Bashsoft.IO;
using Bashsoft.IO.Contracts;
using Bashsoft.Repository;
using BashSoft;
using System;

namespace Bashsoft
{
    class Laucher
    {
        static void Main(string[] args)
        {
            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositorySorter(), new RepositoryFilter());

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            OutputWriter.WriteMessageOnNewLine("Please enter a coomand OR type 'help' in order to get the full list of commands which are available.");
            reader.StartReadingCommands();
        }
    }
}
