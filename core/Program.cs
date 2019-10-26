﻿using System;
using System.Text;
using Library;


namespace core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Passcore.Core");
            Console.WriteLine("Core Build #21");

            Console.Write("Please Enter pKey_0>");
            string pKey_0 = Console.ReadLine();
            Console.Write("Please Enter pKey_1>");
            string pKey_1 = Console.ReadLine();
            Console.Write("Please Enter pKey_2>");
            string pKey_2 = Console.ReadLine();
            Console.Write("Include character?[y/N]");
            string isHardStr = Console.ReadLine();
            bool isHard;
            switch(isHardStr.ToLower())
            {
                case "y":
                    isHard = true;
                    break;
                case "n":
                    isHard = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.Write("Length?[16/32/64/128/256]");
            int length = Convert.ToInt32(Console.ReadLine().Trim());
            switch (length)
            {
                case 16:
                case 32:
                case 64:
                case 128:
                case 256:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Program program = new Program();
            Console.WriteLine("Password>" + program.EncryptMyPass(pKey_0, pKey_1, pKey_2, isHard));
            //Console.WriteLine(str[0]);
            Console.ReadKey();
        }

        private string EncryptMyPass(string pKey_0, string pKey_1) => EncryptMyPass(pKey_0, pKey_1, "Default", false);
        private string EncryptMyPass(string pKey_0, string pKey_1, string pKey_2) => EncryptMyPass(pKey_0, pKey_1, pKey_2, false);
        private string EncryptMyPass(string pKey_0, string pKey_1, string pKey_2, bool isHard)
        {
            if (string.IsNullOrWhiteSpace(pKey_1) || string.IsNullOrWhiteSpace(pKey_0))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(pKey_2))
            {
                pKey_2 = "Default";
            }
            Encrypt encrypt = new Encrypt();
            string[] EncryptPool = new string[25]; //General encrypt pool
            string[] s512Pool = new string[] { encrypt.SHA512(pKey_0), encrypt.SHA512(pKey_1), encrypt.SHA512(pKey_2) }; //SHA512 Encrypt Pool
            EncryptPool[0] = ExtractStr(s512Pool[0]);
            EncryptPool[1] = encrypt.MD5(s512Pool[1]);
            EncryptPool[2] = ExtractStr(s512Pool[2]);

            //Make preparation
            EncryptPool[3] = EncryptPool[2] + EncryptPool[1] + EncryptPool[0];
            EncryptPool[4] = ExtractStr(encrypt.MD5(encrypt.SHA256(EncryptPool[3])));
            EncryptPool[5] = s512Pool[1] + s512Pool[2];

            //Make 256bit string * 3
            EncryptPool[6] = encrypt.SHA512(s512Pool[2] + pKey_1 + (int)s512Pool[0].ToCharArray()[0] + EncryptPool[5]);
            EncryptPool[7] = encrypt.SHA384(EncryptPool[4] + (int)s512Pool[2].ToCharArray()[0] + pKey_0);
            EncryptPool[8] = encrypt.SHA512(EncryptPool[5] + (int)s512Pool[0].ToCharArray()[0] + pKey_0);

            //Make upper word
            EncryptPool[8] = UpperSomething(EncryptPool[8]);
            //EncryptPool[7] = EncryptPool[7];
            EncryptPool[6] = UpperSomething(EncryptPool[6]);
           
            string corePass = AddLikeX(AddLikeX(EncryptPool[8].Substring(0, 64), EncryptPool[7].Substring(0, 64)), EncryptPool[6].Substring(0, 128)); //result 256bit encrypted string
            if (isHard == false)
                return corePass;
            else
                return CharSomething(corePass);

        }

        string ExtractStr(string str) => new string(ExtractChar(str.ToCharArray()));


        private char[] ExtractChar(char[] charPool)
        {
            int step, start = 0;
        gen:
            step = charPool[start] % 10;
            if (step == 0)
            {
                start += 1;
                goto gen;
            }
            for (int loop = 0; loop < charPool.Length; loop++)
            {
                if (loop % step == 0)
                {
                    charPool[loop] = default(char);
                }
            }
            return charPool;
        }

        static char[,] SpecialChar = new char[,]
        {
            //I'm very very very ELEGANT!
            //Special Char Pool -> 3x10
            { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*' },
            { '+', '-', '.', '/', ':', ';', '<', '=', '>', '?' },
            { '@', '[', '\\', ']', '^', '_', '{', '|', '}', '~' }
        };

        static string UpperSomething(string str)
        {
            int step, step_2;
            int start = 0;
            int start_2 = 3;
            char[] upperPool = str.ToCharArray();
        generalStep:
            step = upperPool[start] % 10;
            step_2 = upperPool[start_2] % 10;
            if (step == 0)
            {
                start += 1;
                goto generalStep;
            }
            if (step_2 == 0)
            {
                start_2 += 1;
                goto generalStep;

            }
            for (int loop = 0; loop < upperPool.Length; loop++)
            {
                if (loop % step == 0 || loop % step_2 == 0)
                {
                    upperPool[loop] = upperPool[loop].ToString().ToUpper().ToCharArray()[0];
                }
            }

            return new string(upperPool);
        }

        static string CharSomething(string str)
        {
            int step;
            int start = 1;
            char[] charPool = str.ToCharArray();
        gen:
            step = charPool[charPool[start]] % 10;
            if ((step == 0) || (step < 4 || step > 7))
            {
                start += 1;
                goto gen;
            }
            for (int loop = 0; loop < charPool.Length; loop++)
            {
                if (loop % step == 0)
                {
                    charPool[loop] = SpecialChar[step - 5, charPool[loop] % 10];
                }
            }

            return new string(charPool);
        }

        string AddLikeX(string str1, string str2)
        {
            if (str1.Length != str2.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            char[] c1 = str1.ToCharArray();
            char[] c2 = str2.ToCharArray();
            string r = "";
            for (int i = 0; i < str2.Length; i++)
            {
                r = r + c1[i] + c2[i];
            }
            return r;
        }

    }
}
