﻿namespace eShop.API.DTO;

public class ProductCategoryPostDTO
{
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
}
public class ProductCategoryDeleteDTO : ProductCategoryPostDTO
{
}
public class ProductCategoryGetDTO : ProductCategoryPostDTO
{
}

public class ProductCategorySmallGetDTO
{
    public int CategoryId { get; set; }
}