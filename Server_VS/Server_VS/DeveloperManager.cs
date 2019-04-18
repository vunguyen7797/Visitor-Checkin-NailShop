using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_VS
{
    class DeveloperManager
    {
        #region Const
        const int Col = 9;
        const int Row = 3;
        #endregion

        #region Properties
        private char[,] de_cry_arr = new char[Row, Col];
        public char[,] De_cry_arr
        {
            get
            {
                return de_cry_arr;
            }

            set
            {
                de_cry_arr = value;
            }
        }
        #endregion

        #region Init
        public DeveloperManager()
        {
            int ascii_key = 65;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (ascii_key > 90) ascii_key = 35; //  # in ascii
                    De_cry_arr[i, j] = Convert.ToChar(ascii_key);
                    ascii_key++;
                }
            }
        }
        #endregion

        #region Methods
        public string Encrytion(string pass) // encrypt password
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (pass.IndexOf(De_cry_arr[i, j]) >= 0) // password contains a character from DeCryp alphabet
                    {
                        pass = pass.Replace(De_cry_arr[i, j].ToString(), j + "" + i); // Replace each other
                    }
                }
            }
            return pass;
        }
        public string De_Encrytion(string pass)
        {

            return pass;
        }
        public bool Check_Account(string ID, string pass)
        {
            pass = Encrytion(pass);
            for (int i = 0; i < Cons.ID.Length; i++)
            {
                if (ID == Cons.ID[i]) break;
                if (i == Cons.ID.Length - 1) 
                {
                    return false;
                }
            }
        
            for (int i = 0; i < Cons.pass_key.Length; i++)
            {
                if (pass == Cons.pass_key[i]) break;
                if (i == Cons.pass_key.Length - 1)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
