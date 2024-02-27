using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_Assessment_3
{
    class Changes
    {
        public int lineNumber; //variable to record the different line's number
        public string[] line; //the array that will be stored, either a sentence or file
        public string excess; //variable to record the extra lines
        public List<string> listLine; //list for any array that had to be modified in FileChecks
        public int sentencePosition; //position of words that were different
        public ConsoleColor colour; //colour that the word will be highlighted in
        public int fileAEnd; //end of the short file/sentence
        public int fileBEnd; //end of the long file/sentence
        public bool isArray; //bool condition for if statements in the main program
        public bool isConstructorA; //bool condition for if statements in the main program
        public Changes(int LineNumber, string[] Line, int SentencePosition, ConsoleColor Colour, bool IsArray, bool IsConstructorA) //constructorA for the string array
        {
            lineNumber = LineNumber; //constructor takes the line in which the differences were found, the string array, the position of the word that was different, the colour needed to highlight and bools to check which constructor it is
            line = Line;
            sentencePosition = SentencePosition;
            colour = Colour;
            isArray = IsArray;
            isConstructorA = IsConstructorA;
        }
        public Changes(string Excess, string[] Line, int FileAEnd, int FileBEnd, ConsoleColor Colour, bool IsArray, bool IsConstructorA) //constructorB for the string array
        {
            excess = Excess; //constructor takes the line number with excess words, or stores line number+ for any excess lines. It will then contain the same info as the above constructor
            line = Line;
            fileAEnd = FileAEnd;
            fileBEnd = FileBEnd;
            colour = Colour;
            isArray = IsArray;
            isConstructorA = IsConstructorA;
        }
        public Changes(string Excess, List<string> ListLine, int FileAEnd, int FileBEnd, ConsoleColor Colour, bool IsArray)
        {
            excess = Excess; //constructor that does the same as the second Changes() constructor but takes a list instead
            listLine = ListLine;
            fileAEnd = FileAEnd;
            fileBEnd = FileBEnd;
            colour = Colour;
            isArray = IsArray;
        }
        //polymorphism for each constructor
    }
}
