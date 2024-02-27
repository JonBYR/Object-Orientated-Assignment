using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_Assessment_3
{
    class TextColour
    {
        private string _text;
        public string Text //encapsulation of the _text and _colour variables
        {
            get {return _text; }
            set {_text = value; }
        }
        private ConsoleColor _colour;
        public ConsoleColor Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }
        public void ColouredText() //from the encapsulated variables the word will change to the required colour, then be reset so that any other words will be the default colour
        {
            Console.ForegroundColor = _colour;
            Console.Write($"{_text} ");
            Console.ResetColor();
        }
    }
}
