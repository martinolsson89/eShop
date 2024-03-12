using System.Text;
using System.Text.Json;

namespace eShop.UI.Http.Clients
{
    public class ProductHttpClient
    {
        private readonly HttpClient _httpClient;
        public string _baseAddress = "https://localhost:5501/api/";

        public ProductHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = (new Uri($"{_baseAddress}products"));

        }


        public async Task<List<ProductGetDTO>> GetProductsAsync(int categoryId)
        {
            try
            {
                // Use the relative path, not the base address here
                string relativePath = $"productsbycategory/{categoryId}";
                using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
                response.EnsureSuccessStatusCode();

                var resultStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? [];
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        // Overload, if no category is specified, retrieve all cars.
        public async Task<List<ProductGetDTO>> GetProductsAsync()
        {
            try
            {
                // Use the relative path, not the base address here
                string relativePath = "products";
                using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
                response.EnsureSuccessStatusCode();

                var resultStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? [];
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        //function to delete a product
        public async Task DeleteProductAsync(int id)
        {
            try
            {
                // Use the relative path, not the base address here
                string relativePath = $"products/{id}";
                using HttpResponseMessage response = await _httpClient.DeleteAsync(relativePath);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                //return Results.NotFound();
            }
        }

        public async Task EditProductAsync(ProductPutDTO product)
        {
            //edit a product
            try
            {
                // Use the relative path, not the base address here
                string relativePath = $"products/{product.Id}";
                var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await _httpClient.PutAsync(relativePath, productJson);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                //return Results.NotFound();
            }
        }

        public async Task<List<BrandGetDTO>> GetBrandsAsync()
        {
            try
            {
                // Use the relative path, not the base address here
                string relativePath = "brands/";
                using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
                response.EnsureSuccessStatusCode();

                var resultStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<List<BrandGetDTO>>(resultStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? [];
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        //GetBrandByIdAsync
        public async Task<BrandGetDTO> GetBrandByIdAsync(int Id)
        {
            try
            {
                // Use the relative path, not the base address here
                string relativePath = $"brands/{Id}";
                using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
                response.EnsureSuccessStatusCode();

                var resultStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<BrandGetDTO>(resultStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new BrandGetDTO();
            }
            catch (Exception ex)
            {
                return new BrandGetDTO();
            }
        }


        


        

    }
}
