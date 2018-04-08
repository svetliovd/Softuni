using Bashsoft.Attributes;
using Bashsoft.Exceptions;
using BashSoft;
using System.Diagnostics;

namespace Bashsoft.IO.Commands
{
    [AliasAttribute("open")]
    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            else
            {
                string fileName = this.Data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
        }
    }
}
