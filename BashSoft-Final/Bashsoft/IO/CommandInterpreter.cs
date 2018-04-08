using System;
using BashSoft;
using Bashsoft.Exceptions;
using Bashsoft.IO.Commands;
using Bashsoft.IO.Contracts;
using System.Reflection;
using System.Linq;
using Bashsoft.Attributes;

namespace Bashsoft.IO
{
    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpredCommand(string input)
        {
            string[] data = input.Split(' ');
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }
        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            object[] parametersForConstruction = new object[]
            {
                input, data
            };

            Type typeOfCommand =
                Assembly.GetExecutingAssembly()
                .GetTypes()
                .First(type => type.GetCustomAttributes(typeof(AliasAttribute))
                .Where(atr => atr.Equals(command))
                .ToArray().Length > 0);

            Type typeOfInterpreter = typeof(CommandInterpreter);

            Command exe = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstruction);

            FieldInfo[] fieldsOfCommand = typeOfCommand.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            FieldInfo[] fieldsOfInterpreter = typeOfInterpreter.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var fieldOfCommand in fieldsOfCommand)
            {
                Attribute atrAttribute = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));
                if (atrAttribute != null)
                {
                    if (fieldsOfInterpreter.Any(x => x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(exe, fieldsOfInterpreter
                            .First(x => x.FieldType == fieldOfCommand.FieldType)
                            .GetValue(this));
                    }
                }
            }
            return exe;
        }

     //   private void TryDropDb(string input, string[] data)
     //   {
     //       if (data.Length != 1)
     //       {
     //           this.DisplayInvalidCommandMessage(input);
     //           return;
     //       }
     //
     //       this.repository.UnloadData();
     //       OutputWriter.WriteMessageOnNewLine("Database dropped!");
     //   }

    //    private void TryOrderAndTake(string input, string[] data)
    //    {
    //        if (data.Length == 5)
    //        {
     //           string courseName = data[1];
    //            string comparison = data[2].ToLower();
    //            string takeCommand = data[3].ToLower();
    //            string takeQuantity = data[4].ToLower();
    //
    //            this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
    //        }
    //        else
     //       {
    //            this.DisplayInvalidCommandMessage(input);
    //        }
    //    }

       // private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string comparison)
       // {
       //     if (takeCommand == "take")
       //     {
       //         if (takeQuantity == "all")
       //         {
       //             this.repository.OrderAndTake(courseName, comparison);
       //         }
       //         else
       //         {
       //             int studentsToTake;
       //             bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
       //             if (hasParsed)
       //             {
       //                 this.repository.OrderAndTake(courseName, comparison, studentsToTake);
       //             }
       //             else
       //             {
       //                 OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
       //             }
       //         }
       //     }
       //     else
       //     {
       //         OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
       //     }
       // }

     //   private void TryFilterAndTake(string input, string[] data)
     //   {
      //      if (data.Length == 5)
      //      {
      //          string courseName = data[1];
      //          string filter = data[2].ToLower();
      //          string takeCommand = data[3].ToLower();
       //         string takeQuantity = data[4].ToLower();
       //
       //         this.TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
       //     }
       //     else
       //     {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
       // }

        //private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            this.repository.FilterAndTake(courseName, filter);
        //        }
        //       else
        //        {
        //            int studentsToTake;
        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
        //
        //            if (hasParsed)
        //            {
         //               this.repository.FilterAndTake(courseName, filter, studentsToTake);
         //           }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
         //       }
         //   }
         //   else
         //   {
          //      OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
         //   }
        //}

     //   private void TryShowWantedData(string input, string[] data)
     //   {
     //       if (data.Length == 2)
      //      {
      //          string courseName = data[1];
      //          this.repository.GetAllStudentsFromCourse(courseName);
     //       }
     //       else if (data.Length == 3)
      //      {
     //           string courseName = data[1];
     //           string userName = data[2];
     //           this.repository.GetStudentScoresFromCourse(courseName, userName);
     //       }
     //       else
     //       {
     //           DisplayInvalidCommandMessage(input);
      //      }
      //  }

        //private void TryGetHelp(string input, string[] data)
        //{
            //OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "(the output is written on the console)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "(the output is written on the console)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
            //OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
       // }

   //     private void TryReadDatabaseFromFile(string input, string[] data)
   //     {
   //         if (data.Length == 2)
   //         {
   //             string fileName = data[1];
    //            this.repository.LoadData(fileName);
    //        }
    //        else
    //        {
    //            this.DisplayInvalidCommandMessage(input);
    //        }
    //    }

   //     private void TryChangePathAbsolute(string input, string[] data)
   //     {
    //        if (data.Length == 2)
    //        {
    //            string absolutePath = data[1];
    //            this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
    //        }
     //       else
     //       {
     //           this.DisplayInvalidCommandMessage(input);
     //       }
      //  }

     //   private void TryChangePathRelatively(string input, string[] data)
     //   {
      //      if (data.Length == 2)
      //      {
      //          string relPath = data[1];
      //          this.inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
      //      }
      //      else
      //      {
       //         this.DisplayInvalidCommandMessage(input);
      //      }
      //  }

    //    private void TryCompareFiles(string input, string[] data)
    //    {
     //       if (data.Length == 3)
     //       {
     //           string firstPath = data[1];
     //           string secondPath = data[2];
     //
     //           this.judge.CompareContent(firstPath, secondPath);
     //       }
     //       else
     //       {
     //           this.DisplayInvalidCommandMessage(input);
     //       }
     //   }

     //   private void TryTraverseFolder(string input, string[] data)
     //   {
     //       if (data.Length == 1)
      //      {
      //          this.inputOutputManager.TraverseDirectory(0);
      //      }
      //      else if (data.Length == 2)
      //      {
      //          int depth;
      //          bool hasParsed = int.TryParse(data[1], out depth);
      //          if (hasParsed)
      //          {
      //              this.inputOutputManager.TraverseDirectory(depth);
      //          }
      //          else
      //          {
      //              OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
      //          }
      //      }
      //      else
      //      {
      //          this.DisplayInvalidCommandMessage(input);
      //      }
       // }

    //    private void TryCreateDirectory(string input, string[] data)
    //    {
    //        if (data.Length == 2)
    //        {
    //            string folderName = data[1];
    //            this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
    //        }
    //        else
    //        {
    //            this.DisplayInvalidCommandMessage(input);
    //        }
    //    }

    //    private void DisplayInvalidCommandMessage(string input)
    //    {
     //       OutputWriter.WriteMessageOnNewLine($"The command '{input}' is invalid");
     //   }

     //   private void TryOpenFile(string input, string[] data)
     //   {
     //       if (data.Length == 2)
     //       {
     //           string fileName = data[1];
     //           Process.Start(SessionData.currentPath + "\\" + fileName);
     //      }
     //       else
     //       {
     //           this.DisplayInvalidCommandMessage(input);
     //       }
     //   }
    }
}
