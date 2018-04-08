using System;
using BashSoft;
using Bashsoft.Exceptions;
using Bashsoft.IO.Contracts;

namespace Bashsoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        public Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }

        protected string[] Data
        {
            get { return data; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                data = value;
            }
        }

        protected string Input
        {
            get { return input; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                input = value;
            }
        }

        public abstract void Execute();
    }
}
