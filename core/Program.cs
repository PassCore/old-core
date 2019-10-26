using System;
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

            core.Core cc = new Core();
            Console.WriteLine("Password>" + cc.EncryptMyPass(pKey_0, pKey_1, pKey_2, isHard).Substring(0, length));
            //Console.WriteLine(str[0]);
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

    }
}
