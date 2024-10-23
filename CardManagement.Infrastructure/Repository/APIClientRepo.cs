

using CardManagement.Application.Dtos.Card;
using CardManagement.Application.IRepository;
using System.Net.Http.Json;

namespace CardManagement.Infrastructure.Repository
{
    public class APIClientRepo : IAPIClientRepo
    {
        private readonly HttpClient _httpClient;

        public APIClientRepo(HttpClient httpClient) =>
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
       

        public async Task<bool> PostCardApplicationCall(CardApplicationRequest request)
        {
            var requestPayload = new
            {
                FirstName = request.Firstname,
                LastName = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                IdPhoto = request.IDPhoto.FileName
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.com/applyforcard", requestPayload);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

    }
}
