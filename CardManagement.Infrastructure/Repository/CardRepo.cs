using Azure.Core;
using CardManagement.Application.Dtos.Card;
using CardManagement.Application.Dtos.SysUser;
using CardManagement.Application.IRepository;
using CardManagement.Domain.Model;
using CardManagement.Domain.Utils;
using CardManagement.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardManagement.Infrastructure.Repository
{
    public class CardRepo : ICardRepo
    {
        private readonly AppDbContext _dbContext;
        public CardRepo(AppDbContext dbContext) =>
        (_dbContext) = (dbContext ?? throw new ArgumentNullException(nameof(dbContext)));

        public async Task<Card> SaveCardAsync(Card card)
        {
            try
            {
                var oldCard = await GetCardByNumber(card.CardNumber);
                if (oldCard == null)
                {
                    await _dbContext.Cards.AddAsync(card);
                    await _dbContext.SaveChangesAsync();
                    return card;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Card> SetCardPinAsync(SetPinRequest pinRequest)
        {
            try
            {
                var card = await GetCardByNumber(pinRequest.CardNumber);
                if (card != null && card.Status != Enums.CardStatus.Blocked)
                {
                    card.PIN = pinRequest.Pin;
                    _dbContext.Cards.Update(card);
                    await _dbContext.SaveChangesAsync();
                }

                return card;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Card> AddBalanceAsync(AddBalanceRequest addBalance)
        {
            try
            {
                var card = await GetCardByNumber(addBalance.CardNumber);
                if (card != null && card.Status ==  Enums.CardStatus.Active)
                {
                    card.Balance = card.Balance + addBalance.Amount;
                    _dbContext.Cards.Update(card);
                    await _dbContext.SaveChangesAsync();
                }
               
                return card;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Card> BlockCardAsync(string CardNumber)
        {
            try
            {
                var card = await GetCardByNumber(CardNumber);
                if (card != null && card.Status != Enums.CardStatus.Blocked)
                {
                    card.Status = Enums.CardStatus.Blocked;
                    _dbContext.Cards.Update(card);
                    await _dbContext.SaveChangesAsync();
                }
               
                return card;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Card> TransferCardBalanceAsync(TransferBalanceRequest transferBalance)
        {
            try
            {
                var fromCard = await GetCardByNumber(transferBalance.FromCardNumber);
                var toCard= await GetCardByNumber(transferBalance.ToCardNumber);
                if (fromCard != null && toCard != null && fromCard.Status == Enums.CardStatus.Active 
                    && toCard.Status == Enums.CardStatus.Active
                    && fromCard.Balance > transferBalance.TransferAmount)
                {
                    fromCard.Balance = fromCard.Balance - transferBalance.TransferAmount;
                    toCard.Balance = toCard.Balance + transferBalance.TransferAmount;
                    _dbContext.Cards.Update(fromCard);
                    _dbContext.Cards.Update(toCard);
                    await _dbContext.SaveChangesAsync();
                }

                return fromCard;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Card> GetCardByNumber(string cardNumber)
        {
            var card = await _dbContext.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
            return card;
        }
    }
}
