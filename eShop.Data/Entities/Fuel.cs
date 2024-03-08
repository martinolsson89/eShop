namespace eShop.Data.Entities;

public class Fuel : IEntity
{
    public int Id { get; set; }
    public string FuelName { get; set; } = string.Empty;
    public OptionType? OptionType { get; set; }
    public List<Product> Products { get; set; } = [];
}