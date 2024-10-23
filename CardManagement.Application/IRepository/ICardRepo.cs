using CardManagement.Application.Dtos.Card;
using CardManagement.Application.Dtos.SysUser;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.IRepository
{
    public interface ICardRepo
    {
        public Task<Card> SaveCardAsync(Card card);
        public Task<Card> SetCardPinAsync(SetPinRequest pinRequest);
        public Task<Card> AddBalanceAsync(AddBalanceRequest addBalance);
        public Task<Card> BlockCardAsync(string CardNumber);
        public Task<Card> TransferCardBalanceAsync(TransferBalanceRequest transferBalance);

    }
}
