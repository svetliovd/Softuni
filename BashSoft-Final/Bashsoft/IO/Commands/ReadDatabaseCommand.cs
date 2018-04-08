﻿using Bashsoft.Exceptions;
using Bashsoft.Attributes;
using Bashsoft.IO.Contracts;

namespace Bashsoft.IO.Commands
{
    [AliasAttribute("readdb")]
    public class ReadDatabaseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ReadDatabaseCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            this.repository.LoadData(fileName);
        }
    }
}
