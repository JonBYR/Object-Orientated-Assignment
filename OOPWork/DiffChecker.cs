using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_Assessment_3
{
    class DiffChecker : Exception //class created for custom exception handling
    {
        public DiffChecker(string message) : base(message)
        {

        }
    }

}
