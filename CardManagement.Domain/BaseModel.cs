using CardManagement.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardManagement.Domain.Utils;

namespace CardManagement.Domain
{
    public class BaseModel
    {
        [Required]
        public DateTime Created_At { get; set; } = DateTime.Now;

        public DateTime? Updated_At { get; set; }

        [Required]
        public Enums.ActiveStatus IsActive { get; set; } = Enums.ActiveStatus.Active;

    }
}
