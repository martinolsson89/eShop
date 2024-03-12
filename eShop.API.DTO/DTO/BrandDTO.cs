using eShop.Data.Shared.Enums;

namespace eShop.API.DTO;

public class BrandPostDTO
{
    public string BrandName { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}

public class BrandPutDTO : BrandPostDTO
{
    public int Id { get; set; }
}

public class BrandGetDTO : BrandPutDTO
{

}