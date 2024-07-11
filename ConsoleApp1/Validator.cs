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

        public static bool ValidateId(int id, List<Item> list) {
            if (id < 0 || list.Contains(new Item(id, "dummyType", 1)))
            {
                return false;
            }
            return true;
        }

        public static bool ContainsId(int id, List<Item> list)
        {
            if (id >= 0 && list.Contains(new Item(id, "dummyType", 1)))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                return false;
            }
            return true;
        }

        public static bool ValidateType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return false;
            }
            return true;
        }
    }
}
