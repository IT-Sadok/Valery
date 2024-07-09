using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ContainsId : VerifyId
    {

        public bool containsId(int id, List<Item> list, out int uniqueId) {
            while (list.Contains(new Item(id, "dummyType", 1)))
            {
                if (list.Contains(new Item(id, "dummyType", 1)))
                {
                    Console.WriteLine("Id is not unique");
                    Console.WriteLine("Please pass a new Id");

                    if(Int32.TryParse(Console.ReadLine(), out id);
                    verifyId(id, out id);
                }
            }
            uniqueId = id;
            return true;

        }

    }
}
