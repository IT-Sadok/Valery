using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class RemoveId : IRemoveId
    {
        public void removeId(int id, List<Item> list) {

            if (list.Remove(new Item(id, "dummyType", 0)))
            {
                Console.WriteLine("Item with Id {0} is Removed! Please press any key to proceed...", id);
            }
            else
            {
                Console.WriteLine("Item with Id {0} is not found. Please press any key to proceed...", id);
            }
        }
    }
}
