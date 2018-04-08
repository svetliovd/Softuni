using BashSoft;
using Bashsoft.Exceptions;
using Bashsoft.IO.Contracts;


namespace Bashsoft.IO.Commands
{
    using Bashsoft.Attributes;

    [Alias("ls")]
    public class TraverseFolderCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public TraverseFolderCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1)
            {
                this.inputOutputManager.TraverseDirectory(0);
            }
            else if (this.Data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(this.Data[1], out depth);
                if (hasParsed)
                {
                    this.inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
