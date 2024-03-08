using eShop.Data.Shared.Enums;

namespace eShop.API.DTO;

public class OptionDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public OptionType OptionType { get; set; }
    public bool IsSelected { get; set; }
    //public int FilterId { get; set; }
}