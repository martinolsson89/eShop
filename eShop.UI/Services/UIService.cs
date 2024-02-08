

namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp)
{
    public List<CategoryGetDTO> Categories { get; set; } = [];
    public List<LinkGroup> CategoryLinkGroups { get; private set; } = 
    [
        new LinkGroup{Name = "Categories"}
    ];
    public int CurrentCategoryId { get; set; }
}