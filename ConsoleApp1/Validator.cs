using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class Validator
    {
        public static bool ValidateId(int id, List<Item> list, out int verifiedAndUniqueId)
        {
            while (list.Contains(new Item(id, "dummyType", 1)) || id<0)
            {
                if (id < 0)
                {
                    Console.WriteLine("Id cannot be <0");
                    Console.WriteLine("Please pass a new Id");
                    Int32.TryParse(Console.ReadLine(), out id);
                    if (list.Contains(new Item(id, "dummyType", 1)))
                    {
                        Console.WriteLine("Id is not unique");
                        Console.WriteLine("Please pass a new Id");
                        Int32.TryParse(Console.ReadLine(), out id);
                    }
                }
            }
            verifiedAndUniqueId = id;
            return true;
        }

        public static bool ContainsId(int id, List<Item> list)
        {
            if (list.Contains(new Item(id, "dummyType", 1)))
            {
                return true;
            }
            return false;
        }
        public static bool ValidateQuantity (int quantity, out int verifiedQuantity)
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
        public static bool ValidateType(string type, out string verifiedType)
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
