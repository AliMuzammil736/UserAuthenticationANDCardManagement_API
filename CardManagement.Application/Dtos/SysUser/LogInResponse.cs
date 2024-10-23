using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Dtos.SysUser
{
    public class LogInResponse
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
