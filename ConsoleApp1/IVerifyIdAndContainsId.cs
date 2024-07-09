using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface IVerifyIdAndContainsId : IVerifyId, IContainsId
    {
        bool VerifyIdAndContainsId(int id, out int verifiedAndUniqueId);
    }
}
