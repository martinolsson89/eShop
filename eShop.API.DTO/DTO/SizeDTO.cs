using eShop.Data.Shared.Enums;

namespace eShop.API.DTO;

public class SizePostDTO
{
    public string Name { get; set; }
    public OptionType OptionType { get; set; }
}
public class SizePutDTO : SizePostDTO
{
    public int Id { get; set; }
}
public class SizeGetDTO : SizePutDTO
{
    public bool IsSelected { get; set; }
}