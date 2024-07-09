using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class VerifyId() : IVerifyId
    {
        public bool verifyId(int id, out int verifiedId)
        {
            while (id < 0)
            {
                    Console.WriteLine("Id cannot be <0");
                    Console.WriteLine("Please pass a new Id");
                    Int32.TryParse(Console.ReadLine(), out id);
            }
            verifiedId = id;
            return true;
        }
    }
}
