﻿namespace eShop.API.DTO;

public class CategoryPostDTO
{
    public string Name { get; set; } = string.Empty;
}

public class CategoryPutDTO : CategoryPostDTO
{
    public int Id { get; set; }
}
public class CategoryGetDTO : CategoryPutDTO
{
   public List<FilterGetDTO>? Filters { get; set; }

}

public class CategorySmallGetDTO : CategoryPutDTO
{
}