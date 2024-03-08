namespace eShop.Data.Entities;

public class ProductFuel
{
    public int ProductId { get; set; }
    public int FuelId { get; set; }
    public Product? Product { get; set; }
    public Fuel? Fuel { get; set; }

}