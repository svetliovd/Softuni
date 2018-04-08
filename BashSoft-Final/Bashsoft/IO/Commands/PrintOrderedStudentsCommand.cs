using BashSoft;
using Bashsoft.Exceptions;
using Bashsoft.IO.Contracts;
using Bashsoft.Attributes;

namespace Bashsoft.IO.Commands
{
    [AliasAttribute("order")]
    public class PrintOrderedStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintOrderedStudentsCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 5)
            {
                throw new InvalidCommandException(this.Input);
            }

            TryOrderAndTake(this.Input, this.Data);
        }

        private void TryOrderAndTake(string input, string[] data)
        {
            string courseName = data[1];
            string comparison = data[2].ToLower();
            string takeCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();

            this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
        }

        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, comparison, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }
    }
}
