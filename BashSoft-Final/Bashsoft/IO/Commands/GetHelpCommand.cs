﻿using BashSoft;
using Bashsoft.Exceptions;
using Bashsoft.IO.Contracts;
using Bashsoft.Attributes;

namespace Bashsoft.IO.Commands
{
    [AliasAttribute("help")]
    public class GetHelpCommand : Command
    {
        public GetHelpCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.DiplsayHelp();
        }

        private void DiplsayHelp()
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - cdrel:relative path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - cdabs:absolute path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readdb: path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excellent/average/poor  take 2/5/all students - filterExcelent"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "(the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "(the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "show course information – show: courseName userName"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "drop database – dropdb"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "display data entities - display student/courses ascending/descending"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
        }
    }
}
