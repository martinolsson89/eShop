namespace eShop.UI.Http.Clients
{
    public class CategoryHttpClient
    {
        private readonly HttpClient _httpClient;
        private string _baseAddress = "https://localhost:7001/api/";

        public CategoryHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = (new Uri($"{_baseAddress}categorys"));

        }
    }
}
