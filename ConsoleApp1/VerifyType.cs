using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class VerifyType : IVerifyType
    {
        public bool verifyType(string type, out string verifiedType)
        {
            while (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Type cannot be null or empty");
                Console.WriteLine("Please pass a new item Type");
                type = Console.ReadLine();
            }
            verifiedType = type;
            return true;
        }
    }
}
