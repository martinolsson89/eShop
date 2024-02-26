namespace eShop.Data.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }

    public List<Category>? Categories { get; set; }
    public List<Color>? Colors { get; set; }
    public List<Size>? Sizes { get; set; }
    public List<Fuel>? Fuels { get; set; }

    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    }
    

