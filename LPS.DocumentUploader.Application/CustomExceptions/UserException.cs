using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.CustomExceptions
{
    public class UserException : Exception
    {
        public UserException()
        {

        }

        public UserException(string message)
            : base(message)
        {

        }
        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public override string Message
        {
            get
            {
                return "Error occured! Call your Administrator for further information.";
            }
        }
    }
}
