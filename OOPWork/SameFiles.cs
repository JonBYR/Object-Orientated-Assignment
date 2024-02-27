using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_Assessment_3
{
    class SameFiles : Exception //class created for custom exception handling
    {
        public SameFiles(string message) : base(message)
        {

        }
    }
}
