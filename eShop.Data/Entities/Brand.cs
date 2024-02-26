namespace eShop.Data.Entities;

public class Brand : IEntity
{
    public int Id { get; set; }
    public string BrandName { get; set; }
    public OptionType? OptionType { get; set; }
    public List<Product> Products { get; set; } = [];
}