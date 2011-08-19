using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace DataSimulator
{
    
    class Methods
    {
        ArrayList al;
        ArrayList bl;
        public Methods()
        {
            al = new ArrayList();
            bl = new ArrayList();
        }
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
        public bool CheckDuplicate(string[] sc)
        {
            if (al.Contains(sc[0]) || al.Contains(sc[1]))
            {
                return false;
            }
            else
            {
                al.Add(sc[0]);
                bl.Add(sc[1]);               
            }
            return true;
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
