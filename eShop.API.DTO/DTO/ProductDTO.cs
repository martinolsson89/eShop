namespace eShop.API.DTO;

public class ProductPostDTO
{
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public int BrandId { get; set; } = 0;
}
public class ProductPutDTO : ProductPostDTO
{
    public int Id { get; set; }
}
public class ProductGetDTO : ProductPutDTO
{
    public List<ColorGetDTO>? Colors { get; set; }
    public List<FuelGetDTO>? Fuels { get; set; }
    public BrandGetDTO? Brand { get; set; }
    public List<FilterGetDTO>? Filters { get; set; }
}
public class CartItemDTO : ProductPutDTO
{
    public ColorGetDTO? Color { get; set; }
    public FuelGetDTO? Fuel { get; set; }
    public BrandGetDTO? Brand { get; set; }

    public List<FilterGetDTO>? Filters { get; set; }
}