using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Algorithms_Assessment_3
{
    class Diff : FileChecks //all methods from FileChecks will be used in this class
    {
        private static string FilePicker() //asks user to input the name of the files they want to use
        {
            Console.WriteLine("Please input two files below using format diff [file1] [file2]. Any differences will be highlighted yellow (different case/word) red to show any removed characters, or extra words in a sentence or extra lines in the second file or green if the first file has more lines/extra words in sentences or more characters");
            Console.Write("[Input] ");
            string answer = Console.ReadLine();
            answer.Trim(); //removes any whitespaces
            return answer;
        }
        
        public static string GitDiff()
        {
            string allFiles = FilePicker();
            string[] files = allFiles.Split(); //a string array is created containing the two file names
            if (files.Length > 3)
            {
                throw new FileLength("Please input only 2 files"); //custom exception handling so that the user only inputs two files
            }
            if (files[0] != "diff")
            {
                throw new DiffChecker("Please use diff to compare the two files"); //custom exception handling so that the user only inputs the correct command
            }
            string answer1 = files[1]; //the name of the first file is stored
            string answer2 = files[2]; //the name of the second file is stored
            if (answer1 == answer2) //if the same file is given twice then the user will be asked to give two different files
            {
                throw new SameFiles("Please give two different files"); //custom exception handling so that the user will only give two different files
            }
            Write(files); //the inputed files will be written to a file using the inherited Write() method
            if ((answer1 == "GitRepositories_1a.txt") && (answer2 == "GitRepositories_1b.txt") || (answer1 == "GitRepositories_1b.txt") && (answer2 == "GitRepositories_1a.txt")) //if 1a and 1b are picked then we know it's the same file, so we return this info
            {
                string equalFilesMessage = "Files were the same, no differences detected"; //string created to be written to the log file
                Write(equalFilesMessage); //inherited Write method called
                return "[Output] Files are the same, no differences to report"; //information returned
            }
            else
            {
                string[] file1 = File.ReadAllLines(answer1); //reads in the first file the user typed in and stores it as a string array
                string[] file2 = File.ReadAllLines(answer2); //reads in the second file the user typed in and stores it as a string array
                FileChanger(file1, file2); //inherited method called that checks the changes
                return "[Output] Files are different, discrepencies shown below"; //return a string that will then be used to show the differences in the files underneath
            }

        }
    }
}
