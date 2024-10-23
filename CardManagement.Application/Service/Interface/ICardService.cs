using CardManagement.Application.Dtos.Card;
using CardManagement.Application.Dtos.SysUser;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Service.Interface
{
    public interface ICardService
    {
        public Task<string> CardApplicationAsync(CardApplicationRequest cardApplication , string UserID);

        public Task<string> SetCardPinAsync(SetPinRequest pinRequest);

        public Task<string> AddBalanceAsync(AddBalanceRequest addBalance);

        public Task<string> BlockCardAsync(string CardNumber);

        public Task<string> TransferCardBalanceAsync(TransferBalanceRequest transferBalance);
        

    }
}
