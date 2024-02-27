using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace Algorithms_Assessment_3
{
    class Program
    {
        public static List<Changes> allChanges = new List<Changes>(); //list of Changes objects created that will store any differences between files

        static void Main(string[] args)
        {
            bool filesFound = false;
            while (filesFound == false) //while the exception handling has caught any errors or the user has started the program, the user will continue to be asked to input two files
            {
                try
                {
                    Console.WriteLine(Diff.GitDiff()); //method called from the Diff class
                    if (allChanges != null) //if the list is not empty
                    {
                        for(int i = 0; i < allChanges.Count(); i++) //loop through the list
                        {
                            if ((allChanges[i].isArray == true) && (allChanges[i].isConstructorA == true)) //if the object was the first constructor
                            {
                                Console.WriteLine($"\n[Output] Line changed: {allChanges[i].lineNumber}"); //write the line number that recorded a change
                                for (int j = 0; j < allChanges[i].line.Length; j++) //iterating through the line array to find the word that needs to be highlighted
                                {
                                    if (j == allChanges[i].sentencePosition) //if j is the same as the position of the word that needs to be highlighted
                                    {
                                        TextColour colouredWord = new TextColour(); //object instantiated
                                        colouredWord.Text = allChanges[i].line[j]; //the text and colour variables will be set with the correct information from the constructor
                                        colouredWord.Colour = allChanges[i].colour;
                                        colouredWord.ColouredText(); //method called to change the colour
                                    }
                                    else
                                    {
                                        Console.Write($"{allChanges[i].line[j]} "); //The word is just printed normally
                                    }
                                }   
                            }
                            else if ((allChanges[i].isArray == true) && (allChanges[i].isConstructorA == false)) //if the object was the second constructor
                            {
                                Console.WriteLine($"\n[Output] Line changed: {allChanges[i].excess}"); //method will work the same as above, except use the excess variable to print the line number
                                for (int k = 0; k < allChanges[i].line.Length; k++)
                                {
                                    if ((allChanges[i].fileAEnd <= k) && (k <= allChanges[i].fileBEnd))
                                    {
                                        TextColour colouredWord = new TextColour(); //each word that is between the ends of both files/file sentences will be written to the console with the required colour
                                        colouredWord.Text = allChanges[i].line[k];
                                        colouredWord.Colour = allChanges[i].colour;
                                        colouredWord.ColouredText(); 
                                        Console.Write("\n");
                                    }
                                    else
                                    {
                                        Console.Write($"{allChanges[i].line[k]} "); //The line is just printed normally
                                    }
                                }
                            }
                            else if (allChanges[i].isArray == false) //if it was the third constructor
                            {
                                Console.WriteLine($"\n[Output] Line changed: {allChanges[i].excess}"); //method will work the same but with a list
                                for (int j = 0; j < allChanges[i].listLine.Count(); j++)
                                {
                                    if ((allChanges[i].fileAEnd <= j) && (j <= allChanges[i].fileBEnd))
                                    {
                                        TextColour colouredWord = new TextColour();
                                        colouredWord.Text = allChanges[i].listLine[j];
                                        colouredWord.Colour = allChanges[i].colour;
                                        colouredWord.ColouredText();
                                        Console.Write("\n");
                                    }
                                    else
                                    {
                                        Console.Write($"{allChanges[i].listLine[j]} ");
                                    }
                                    
                                }
                            }
                        }
                        filesFound = true; //no errors recorded so the code has been executed
                    }
                    else
                    {
                        filesFound = true; //else statement for if the files were the same
                    }
                   
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("File(s) does not exist, please try again");
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Please give two files");
                }
                catch (FileLength e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (DiffChecker e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (SameFiles e)
                {
                    Console.WriteLine(e.Message);
                }
                //any errors will be catched, and the code will executed again as filesFound is still false
            }
        }
    }
}
