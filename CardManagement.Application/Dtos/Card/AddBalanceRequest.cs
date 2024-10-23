using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Dtos.Card
{
    public class AddBalanceRequest
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }
}
