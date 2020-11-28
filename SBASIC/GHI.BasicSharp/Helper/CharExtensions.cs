using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace GHI.BasicSharp.Helper
{
    public class CharExtensions

    {
        public static bool IsDigit(char c)
        {
            if ( !(c < '0' || c > '9'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
