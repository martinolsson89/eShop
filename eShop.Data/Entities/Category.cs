﻿namespace eShop.Data.Entities;

public class Category : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Filter> Filters { get; } = [];
    public List<Product> Products { get; } = [];
}