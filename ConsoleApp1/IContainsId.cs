using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface IContainsId
    {
        bool containsId(int id, List<Item> list, out int uniqueId);
    }
}
