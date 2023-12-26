using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Helpers
{
    public class UserHelper
    {
        public static void SetWrongPasswordCounter()
        {
            WrongPassword.WrongPasswordInput += 1;
        }

        public static int GetWrongPasswordCounter()
        {
            return WrongPassword.WrongPasswordInput;
        }

        public static void ResetWrongPasswordInput()
        {
            WrongPassword.WrongPasswordInput = 0;
        }

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }

    class WrongPassword
    {
        private static int _wrongWrongPasswordInput = 0;

        public static int WrongPasswordInput
        {
            get { return _wrongWrongPasswordInput; }
            set { _wrongWrongPasswordInput = value; }
        }
    }
}
