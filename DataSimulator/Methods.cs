using System;
using System.Collections.Generic;
using System.Text;

namespace DataSimulator
{
    class Methods
    {
        public bool ValidateForm(string[] testStrings)
        {
            foreach (string t in testStrings)
            {
                if (t.Trim() == "")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
