using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.IO;

namespace Algorithms_Assessment_3
{
    class FileChecks
    {
        public static string logFileName = "LogChanges.txt"; //the file that the summaries will be written to is defined
        public static void FileChanger(string[] array1, string[] array2) //method will remove all apostrophies from a file
        {
            List<string> apostrophiedList1 = array1.OfType<string>().ToList(); //two list objects created to manipulate the string arrays
            List<string> apostrophiedList2 = array2.OfType<string>().ToList();
            for (int i = 0; i < apostrophiedList1.Count(); i++) //iterates through each word in the array
            {
                string wordToReplace = apostrophiedList1[i]; //the word that needs to be replaced is stored
                string replacedApostrophy = apostrophiedList1[i].Replace("'", " "); //if an apostropy is found, it is replaced with a space
                apostrophiedList1[apostrophiedList1.FindIndex(i => i.Equals(wordToReplace))] = replacedApostrophy; //the word will be replaced with the unapostrophied word at the specific index
            }
            for (int i = 0; i < apostrophiedList2.Count(); i++) //same as the above method
            {
                string wordToReplace = apostrophiedList2[i];
                string replacedApostrophy = apostrophiedList2[i].Replace("'", " ");
                apostrophiedList2[apostrophiedList2.FindIndex(i=>i.Equals(wordToReplace))] = replacedApostrophy;
            }
            array1 = apostrophiedList1.ToArray();
            array2 = apostrophiedList2.ToArray(); //lists are turned back into arrays
            
            if (array1.Length != array2.Length)
            {
                sizeDifference(array1, array2); //if the array sizes are not the same then they must contain different information, so they will be dealt with in a method that checks differences in size
                caseDifference(array1, array2); //method checks the case difference
                wordDifference(array1, array2); //method checks the word difference
            }
            else //if they are the same size we only need to check the case and words for both files
            {
                caseDifference(array1, array2); //method checks the case difference
                wordDifference(array1, array2); //method checks the word difference
            }
            
              
        }
        private static void caseDifference(string[] array1, string[] array2) //method checks for case difference
        {
            int i = 0;
            bool caseBool = false; //boolean created
            for (i = 0; i < Math.Min(array1.Length, array2.Length); i++) //loops through each line in the arrays
            {
                string[] sentence1 = array1[i].Split(" "); //splits each line in the first file into a "sentence" which is an array of words
                string[] sentence2 = array2[i].Split(" "); //splits each line in the second file into a "sentence" which is an array of words
                for (int j = 0; j < Math.Min(sentence1.Length, sentence2.Length); j++) //loops through each word in both arrays
                {
                    if (sentence1[j] == sentence2[j]) continue; //if the words are the same then nothing needs to be recorded, so the code will continue
                    else
                    {
                        if (sentence1[j].ToLower() == sentence2[j].ToLower())
                        {
                            caseBool = true; //if this if statement is true then the words must be the same but in a different case so the bool changes to mark this occurence
                            Changes change = new Changes((i + 1), sentence2, j, ConsoleColor.Yellow, true, true); //The word is in a different case and so marked as yellow
                            Program.allChanges.Add(change);
                        } //object created for later use in the main program
                    }
                } 
            }
            if (caseBool == true)
            {
                string caseMessage = "Case difference in the two files"; //summary of the differences given to the write function
                Write(caseMessage);
            }
        }
        private static void wordDifference(string[] array1, string[] array2) //method checks for word difference
        {
            int i = 0;
            bool wordBool = false; //boolean created
            for (i = 0; i < Math.Min(array1.Length, array2.Length); i++) //loops through each line in the arrays
            {
                string[] sentence1 = array1[i].Split(" "); //splits each line in the first file into a "sentence" which is an array of words
                string[] sentence2 = array2[i].Split(" "); //splits each line in the second file into a "sentence" which is an array of words
                for (int j = 0; j < Math.Min(sentence1.Length, sentence2.Length); j++) //loops through each word in both arrays
                {
                    if (sentence1[j] == sentence2[j]) continue; //if the words are the same then nothing will change, and the code will continue
                    else if (sentence1[j].ToLower() != sentence2[j].ToLower()) //if the words are not the same in lower case then we check the size of each word as they are different
                    {
                        wordBool = true; //if this if statement is initiated then the words must be different to each other so the bool changes to mark this occurence
                        if (sentence1[j].Length < sentence2[j].Length) 
                        {
                            Changes change = new Changes((i + 1), sentence2, j, ConsoleColor.Green, true, true);
                            Program.allChanges.Add(change);
                        } //if the length of the word in file one is smaller, then characters were removed in comparison to the word in the second file, and so the colour green is recorded for the word in file 2
                        else if (sentence1[j].Length > sentence2[j].Length)
                        {
                            Changes change = new Changes((i + 1), sentence2, j, ConsoleColor.Red, true, true); //otherwise, more characters were added in comparison to the second file, and so the colour red is recorded for the object for file 2
                            Program.allChanges.Add(change);
                        }
                        else
                        {
                            Changes change = new Changes((i + 1), sentence2, j, ConsoleColor.Magenta, true, true); //if the characters are the same length but not the same word, the colour magenta is used to highlight the different word
                            Program.allChanges.Add(change);
                        }
                    }
                }
            }
            if (wordBool == true)
            {
                string wordsMessage = "Words in the files had differing characters"; //summary of the differences given to the write function
                Write(wordsMessage);
            }
        }
        private static void sizeDifference(string[] array1, string[] array2) //function to check the size difference in two files
        {
            int i = 0;
            bool linesBool = false; //boolean created
            for (i = 0; i < Math.Min(array1.Length, array2.Length); i++) //loops through each line in the array up until a certain point
            {
                string[] sentence1 = array1[i].Split(" "); //splits each line in the first file into a "sentence" which is an array of words
                string[] sentence2 = array2[i].Split(" "); //splits each line in the second file into a "sentence" which is an array of words

                if (sentence2.Length < sentence1.Length) //if the number of words in the second sentence is less than the number of words in the first sentence, then the second file has less words in it's sentence
                {
                    linesBool = true; //should any of the if statements be initiated the bool turns to true
                    int sentence1End = sentence1.Length;
                    int sentence2End = sentence2.Length; //Length of each sentence recorded
                    int lineNumber = i + 1; //line number will be recorded, then turned to a string
                    string extraWords = lineNumber.ToString(); //variable called to say which line has extraWords
                    List<string> extendedSentence = sentence2.OfType<string>().ToList(); //list created to store the new words
                    for (int j = sentence2End; j < sentence1.Length; j++) //for the remaining words in the sentence of the first file
                    {
                        extendedSentence.Add(sentence1[j]); //words added to the list so that we can highlight that these are extra words from the first file that cause the second file's sentence to be shorter
                    }
                    Changes change = new Changes(extraWords, extendedSentence, sentence2End, sentence1End, ConsoleColor.Red, false); //the extra words are stored as an object with this information
                    Program.allChanges.Add(change);
                }
                else if (sentence2.Length > sentence1.Length) //if the opposite is true, then there are more words in the second file's sentence
                {
                    linesBool = true; //same method used with the sentence of file1 being shorter instead
                    int sentence2End = sentence2.Length;
                    int sentence1End = sentence1.Length;
                    int lineNumber = i + 1;
                    string extraWords = lineNumber.ToString();
                    Changes change = new Changes(extraWords, sentence2, sentence1End, sentence2End, ConsoleColor.Green, true, false); //object created with this information instead
                    Program.allChanges.Add(change); //any words that cause the sentence in file 2 to be longer are highlighted
                }
            }
            if (array2.Length < array1.Length) //if there were more lines in the first file
            {
                linesBool = true;
                List<string> extendedLines = array2.OfType<string>().ToList(); //array converted to a list
                int array1End = array1.Length;
                int array2End = array2.Length; //Length of each file recorded
                int lineNumber = i + 1;
                string extraLines = lineNumber.ToString() + "+"; //all lines that are excess as they do not exist in the other file will have the lineNumber stored as the next new line, and a plus sign to denote they are all extra lines
                for (int j = array2End; j < array1.Length; j++) //for each of the remaining lines in the first array
                {
                    extendedLines.Add(array1[j]); //line is added to the list
                }
                Changes change = new Changes(extraLines, extendedLines, array2End, array1End, ConsoleColor.Red, false); //object created storing the position of the new line and the colour. As these lines cause file 2 to be have less lines than file 1 we highlight that these are extra lines in red 
                Program.allChanges.Add(change);
            }
            else if (array2.Length > array1.Length) //if the inverse is true
            {
                linesBool = true;
                int array2End = array2.Length;
                int array1End = array1.Length;
                int lineNumber = i + 1;
                string extraLines = lineNumber.ToString() + "+";
                Changes change = new Changes(extraLines, array2, array1End, array2End, ConsoleColor.Green, true, false); //object created storing the remaining lines from file 2. As these lines casue file 2 to have more lines than file 1, we show that these lines cause file 2 to be longer in green
                Program.allChanges.Add(change);
            }
            if (linesBool == true) //if any of these conditions happen then the number of lines/ words in sentences are different, which will be written to the log file
            {
                string linesMessage = "Difference in line lengths/number of lines detected in the two files"; //summary of the differences given to the write function
                Write(linesMessage);
            }
        }
        public static void Write(string[] inputArray) //Function that will take in a string array that contains the two files the user gave and append it to a log file
        {
            if (!File.Exists(logFileName)) //if the file doesn't already exist, then the computer will create a new file in the folder and add this information
            {
                using (StreamWriter createFile = File.CreateText(logFileName))
                {
                    createFile.Write("[Input] ");
                    foreach (string word in inputArray)
                    {
                        createFile.Write($"{word} "); //each word from the string array will be written on the same line
                    }
                }
            }
            else //if the file already exists then the above information just needs to be appended to the file
            {
                using (StreamWriter createFile = File.AppendText(logFileName))
                {
                    createFile.Write("\n[Input] ");
                    foreach (string word in inputArray)
                    {
                        createFile.Write($"{word} ");
                    }
                }
            }
        }
        public static void Write(string result) //polymorphism of the write function
        {
            using (StreamWriter createFile = File.AppendText(logFileName)) //for any differences found, the file will have the summary of those differences appended to it
            {
                if (result != null) //if the result variable is null then this function will do nothing
                {
                    createFile.WriteLine($"\n[Output] {result}");
                }
            } //as the file will have already been created in the first instance of Write(), we do not need to create the file here as well
        }
    }
}
