namespace eShop.API.DTO;

public class ProductPostDTO
{
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
}
public class ProductPutDTO : ProductPostDTO
{
    public int Id { get; set; }
}
public class ProductGetDTO : ProductPutDTO
{
    public List<ColorGetDTO>? Colors { get; set; }
    public List<SizeGetDTO>? Sizes { get; set; }


    //public List<FilterGetDTO>? Filters { get; set; }
}
public class CartItemDTO : ProductPutDTO
{
    public ColorGetDTO? Color { get; set; }
    public SizeGetDTO? Size { get; set; }

    //public List<FilterGetDTO>? Filters { get; set; }
}