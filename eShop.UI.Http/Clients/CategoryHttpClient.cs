

using System.Text.Json;

namespace eShop.UI.Http.Clients
{
    public class CategoryHttpClient
    {
        private readonly HttpClient _httpClient;
        public string _baseAddress = "https://localhost:7001/api/";

        public CategoryHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = (new Uri($"{_baseAddress}categorys"));

        }

        public async Task<List<CategoryGetDTO>> GetCategoriesAsync()
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode();

                var result = JsonSerializer.Deserialize<List<CategoryGetDTO>>(await response.Content.ReadAsStreamAsync(),
                        new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

                return result ?? [];
            }
            catch (Exception e)
            {

                return [];
            }

        }
    }
}
