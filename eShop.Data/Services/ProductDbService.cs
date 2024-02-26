using AutoMapper;
using eShop.API.DTO;
using eShop.Data.Contexts;
using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data.Services;

public class ProductDbService(EShopContext db, IMapper mapper) : DbService(db, mapper)
{
    // public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    // {
    //     //IncludeNavigationsFor<Filter>();
    //     //IncludeNavigationsFor<Product>();
    //     var result = await base.GetAsync<TEntity, TDto>();
    //     return result;
    // }
    public async Task<List<ProductGetDTO>> GetProductsByCategoryAsync(int categoryId)
    {
        IncludeNavigationsFor<Color>();
        IncludeNavigationsFor<Size>();
        IncludeNavigationsFor<Brand>();
        IncludeNavigationsFor<Fuel>();
        var productIds = GetAsync<ProductCategory>(pc => pc.CategoryId.Equals(categoryId))
            .Select(pc => pc.ProductId);
        var products = await GetAsync<Product>(p => productIds.Contains(p.Id)).ToListAsync();
        return MapList<Product, ProductGetDTO>(products);
    }
    public List<TDto> MapList<TEntity, TDto>(List<TEntity> entities)
        where TEntity : class
        where TDto : class
    {
        return mapper.Map<List<TDto>>(entities);
    }
}