namespace eShop.API.DTO;

public class ProductFuelPostDTO
{
    public int FuelId { get; set; }
    public int ProductId { get; set; }
}
public class ProductFuelDeleteDTO : ProductFuelPostDTO
{
}
public class ProductFuelGetDTO : ProductFuelPostDTO
{
}

public class ProductFuelSmallGetDTO
{
    public int FuelId { get; set; }
}