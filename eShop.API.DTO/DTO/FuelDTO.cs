using eShop.Data.Shared.Enums;
using Microsoft.VisualBasic.FileIO;

namespace eShop.API.DTO;

public class FuelPostDTO
{
    public string FuelName { get; set; }
    public OptionType OptionType { get; set; }
}

public class FuelPutDTO : FuelPostDTO
{
    public int Id { get; set; }
}

public class FuelGetDTO : FuelPutDTO
{
    public bool IsSelected { get; set; }
}