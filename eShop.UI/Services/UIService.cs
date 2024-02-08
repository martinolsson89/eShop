
namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp)
{
    public List<CategoryGetDTO> Categories { get; set; } = [];
    public List<LinkGroup> CategoryLinkGroups { get; private set; } = 
    [
        new LinkGroup
        {
            Name = "Categories",
            LinkOptions = new ()
            {
                new LinkOption {Id = 1, Name = "Sedan", IsSelected = true },
                new LinkOption {Id = 2, Name = "SUV", IsSelected = false },
                new LinkOption {Id = 3, Name = "Sport car", IsSelected = false },
                new LinkOption {Id = 4, Name = "Family car", IsSelected = false }
            }
        }
        
    ];
    public int CurrentCategoryId { get; set; }
}