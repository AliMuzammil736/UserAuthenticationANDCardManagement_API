using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Domain.Utils
{
    public class Enums
    {
        public enum CardStatus
        {
            Active = 1,
            Blocked = 2,
            Freeze = 3,
        }

        public enum ActiveStatus
        {
            Active = 1,
            Deleted = 2,
        }
    }
}
