using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Domain.Model
{
    public class SysUser : IdentityUser
    {
        public ICollection<Card> Cards { get; set; }
    }
}
