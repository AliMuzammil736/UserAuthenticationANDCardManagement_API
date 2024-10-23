using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Dtos.Card
{
    public class TransferBalanceRequest
    {
        [Required]
        public string FromCardNumber { get; set; }

        [Required]
        public string ToCardNumber { get; set; }

        [Required]
        public decimal TransferAmount { get; set; }

    }
}
