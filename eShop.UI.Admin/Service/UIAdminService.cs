using AutoMapper;
using eShop.API.DTO;
using eShop.UI.Http.Clients;
using eShop.UI.Models.Link;

namespace eShop.UI.Admin.Service;

public class UIAdminService(CategoryHttpClient categoryHttp, ProductHttpClient productHttp, IMapper mapper)
{
    public List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<BrandGetDTO> Brands { get; private set; } = [];
    public List<LinkGroup> CaregoryLinkGroups { get; private set; } =
    [
        new LinkGroup
        {
            Name = "Categories"
        }
    ];

    public async Task GetLinkGroup()
    {
        Categories = await categoryHttp.GetCategoriesAsync();
        CaregoryLinkGroups[0].LinkOptions = mapper.Map<List<LinkOption>>(Categories);
        var linkOption = CaregoryLinkGroups[0].LinkOptions.FirstOrDefault();
        linkOption!.IsSelected = true;
    }

    public async Task GetProductsAsync() =>
        Products = await productHttp.GetProductsAsync();

   //Function to delete a product
    public async Task DeleteProductAsync(int id) =>
        await productHttp.DeleteProductAsync(id);

    public async Task EditProductAsync(ProductPutDTO product) =>
        await productHttp.EditProductAsync(product);

    public async Task GetBrandsAsync() =>
        Brands = await productHttp.GetBrandsAsync();

    //GetBrandByIdAsync(SelectedBrandId)
    public async Task<BrandGetDTO> GetBrandByIdAsync(int id) =>
        await productHttp.GetBrandByIdAsync(id);

    public async Task AddProductAsync(ProductGetDTO product) =>
    await productHttp.AddProductAsync(product);

    public async Task AddCategoryAsync(CategoryGetDTO category)
    {
        await categoryHttp.AddCategoryAsync(category);
    }

    public async Task GetCategoriesAsync() =>
        Categories = await categoryHttp.GetCategoriesAsync();
}