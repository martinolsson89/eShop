
namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp, ProductHttpClient productHttp ,IMapper mapper)
{
    public List<CategoryGetDTO> Categories { get; set; } = [];

    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<LinkGroup> CategoryLinkGroups { get; private set; } = 
    [
        new LinkGroup
        {
            Name = "Categories",
            // LinkOptions = new ()
            // {
            //     new LinkOption {Id = 1, Name = "Sedan", IsSelected = true },
            //     new LinkOption {Id = 2, Name = "SUV", IsSelected = false },
            //     new LinkOption {Id = 3, Name = "Sport car", IsSelected = false },
            //     new LinkOption {Id = 4, Name = "Family car", IsSelected = false }
            // }
        }
        
    ];
    public int CurrentCategoryId { get; set; }

    public async Task GetLinkGroup()
    {
        Categories = await categoryHttp.GetCategoriesAsync();
        CategoryLinkGroups[0].LinkOptions = mapper.Map<List<LinkOption>>(Categories);
        var linkOption = CategoryLinkGroups[0].LinkOptions.FirstOrDefault();
    }

    public async Task OnCategoryLinkClick(int id)
    {
        CurrentCategoryId = id;
        await GetProductsAsync();
        CategoryLinkGroups[0].LinkOptions.ForEach(l => l.IsSelected = false);
        CategoryLinkGroups[0].LinkOptions.Single(l => l.Id.Equals(CurrentCategoryId)).IsSelected = true;

        // var filters = Categories.Single(c => c.Id.Equals(CurrentCategoryId)).Filters;
        // FilterGroups = mapper.Map<List<FilterGroup>>(filters);
    }

    public async Task GetProductsAsync() => Products = await productHttp.GetProductsAsync(CurrentCategoryId);


}