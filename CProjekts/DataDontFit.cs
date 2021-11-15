using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CProjekts
{
    public class InputOutOfBondsException : Exception
    {
       public InputOutOfBondsException()
       {

       }
       public InputOutOfBondsException(string message) : base(message)
       {

       }

        public InputOutOfBondsException(string message, Exception outofbonds) : base(message, outofbonds)
        {

        }
    }
}
