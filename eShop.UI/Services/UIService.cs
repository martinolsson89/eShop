
using eShop.UI.Models.Filter;
using eShop.UI.Storage.Services;

namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp, ProductHttpClient productHttp ,IMapper mapper, IStorageService storageService, FilterHttpClient filterHttp)
{
    public List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<FilterGroup> FilterGroups { get; private set; } = [];
    public List<CartItemDTO> CartItems { get; set; } = [];
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
        linkOption!.IsSelected = true;
    }

    public async Task OnCategoryLinkClick(int id)
    {
        CurrentCategoryId = id;
        await GetProductsAsync();

        Products.ForEach(p => p.Colors!.First().IsSelected = true);

        CategoryLinkGroups[0].LinkOptions.ForEach(l => l.IsSelected = false);
        CategoryLinkGroups[0].LinkOptions.Single(l => l.Id.Equals(CurrentCategoryId)).IsSelected = true;

        
        // var filters = Categories.Single(c => c.Id.Equals(CurrentCategoryId)).Filters;
        // FilterGroups = mapper.Map<List<FilterGroup>>(filters);
    }

    public async Task GetProductsAsync() => Products = await productHttp.GetProductsAsync(CurrentCategoryId);

    // Function to Get items from storage
    public async Task<T> ReadStorage<T>(string key)// where T : class
    {
        //if (string.IsNullOrEmpty(key) || storage is null) return new T();
        return await storageService.GetAsync<T>(key);
    }

    // Function to Get single item from storage

    public async Task<T> ReadSingleStorage<T>(string key) => await storageService!.GetAsync<T>(key);


    // Function to Save to storage
    public async Task SaveToStorage<T>(string key, T value)
    {
        if (string.IsNullOrEmpty(key) || storageService is null) 
            return;
        
        await storageService.SetAsync(key, value);
    } 

    // Function to Remove from storage
    public async Task RemoveFromStorage(string key)
    {
        if (string.IsNullOrEmpty(key) || storageService is null) 
            return;
        
        await storageService.RemoveAsync(key);
    }

    public async Task FilterProducts()
    {
        var filterDTOs = FilterGroups
            .Where(group => group.FilterOptions.Any(option => option.OptionType == group.OptionType && option.IsSelected))
            .Select(group => new FilterRequestDTO
            {
                CategoryId = CurrentCategoryId,
                OptionType = group.OptionType,
                Id = group.Id,
                Name = group.Name,
                TypeName = group.TypeName,
                Options = group.FilterOptions
                    .Where(option => option.OptionType == group.OptionType && option.IsSelected)
                    .Select(option => mapper.Map<OptionDTO>(option))
                    .ToList()
            }).ToList();

        if(filterDTOs.Count > 0)
            Products = await filterHttp.FilterProductsAsync(filterDTOs);
    }

}