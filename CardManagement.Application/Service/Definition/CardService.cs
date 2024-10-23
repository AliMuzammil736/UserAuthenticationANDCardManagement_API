using CardManagement.Application.Dtos.Card;
using CardManagement.Application.IRepository;
using CardManagement.Application.Service.Interface;
using CardManagement.Application.Utils;
using CardManagement.Domain.Model;
using Microsoft.Extensions.Configuration;


namespace CardManagement.Application.Service.Definition
{
    public class CardService: ICardService
    {
        private readonly ICardRepo _cardRepo;
        private readonly IConfiguration _configuration;
        private readonly Helper _helper;
        private readonly IAPIClientRepo _clientRepo;

        public CardService(ICardRepo cardRepo, IConfiguration configuration, Helper helper, IAPIClientRepo clientRepo) =>
         (_cardRepo, _configuration, _clientRepo, _helper) = (cardRepo ?? throw new ArgumentNullException(nameof(cardRepo)),
                                            configuration ?? throw new ArgumentNullException(nameof(configuration)),
                                            clientRepo?? throw new ArgumentNullException(nameof(clientRepo)),
                                            helper ?? throw new ArgumentNullException(nameof(helper)));

        public async Task<string> CardApplicationAsync(CardApplicationRequest cardApplication, string UserID)
        {

            if(!_helper.IsValidFileExtension(cardApplication.IDPhoto))
            {
                return "Invalid ID Photo type";
            }
            
            var ApiCall = await _clientRepo.PostCardApplicationCall(cardApplication);

            var result = await _cardRepo.SaveCardAsync(new Card
            {
                Id = Guid.Empty,
                CardNumber = new string(Enumerable.Range(0, 15).Select(_ => (char)new Random().Next('0', '9' + 1)).ToArray()),
                ExpiryDate = DateOnly.FromDateTime(DateTime.Now.Date.AddYears(5)),
                CVV = new Random().Next(100, 999).ToString(),
                Balance = 0,
                FirstName = cardApplication.Firstname,
                LastName = cardApplication.Lastname,
                PhoneNumber = cardApplication.PhoneNumber,
                UserId = UserID
            });
            return "Card Applied Successfully";
        }


        public async Task<string> SetCardPinAsync(SetPinRequest pinRequest)
        {
           var card =  await _cardRepo.SetCardPinAsync(pinRequest);
            if (card == null) return "Error Setting Card Pin";
            else return "Card Pin has been Updated Successfully";
        }

        public async Task<string> AddBalanceAsync(AddBalanceRequest addBalance)
        {
            var card = await _cardRepo.AddBalanceAsync(addBalance);
            if (card == null) return "Error Adding Balance";
            else return "Balance has been Added Successfully";
        }

        public async Task<string> BlockCardAsync(string CardNumber)
        {
            var card = await _cardRepo.BlockCardAsync(CardNumber);
            if (card == null) return "Error Blocking Card";
            else return "Card has been Blocked Successfully";
        }

        public async Task<string> TransferCardBalanceAsync(TransferBalanceRequest transferBalance)
        {
            var card = await _cardRepo.TransferCardBalanceAsync(transferBalance);
            if (card == null) return "Error Transferring Balance";
            else return "Balance has been Transferred Successfully";
        }

    }
}

