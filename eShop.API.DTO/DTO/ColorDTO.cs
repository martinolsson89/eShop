using eShop.Data.Shared.Enums;

namespace eShop.API.DTO;

public class ColorPostDTO
{
    public string Name { get; set; }
    public OptionType OptionType { get; set; }
    public string ColorHex { get; set; } = string.Empty;
    public string BkColorHex { get; set; } = string.Empty;
}
public class ColorPutDTO : ColorPostDTO
{
    public int Id { get; set; }
}
public class ColorGetDTO : ColorPutDTO
{
    public bool IsSelected { get; set; }
}
