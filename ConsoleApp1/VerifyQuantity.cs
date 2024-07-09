using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class VerifyQuantity : IVerifyQuantity
    {
        public bool verifyQuantity(int quantity, out int verifiedQuantity)
        {
            while (quantity <= 0)
            {
                Console.WriteLine("Quantity cannot be <=0");
                Console.WriteLine("Please pass a new Quantity");
                Int32.TryParse(Console.ReadLine(), out quantity);
            }
            verifiedQuantity = quantity;
            return true;
        }
    }
}
