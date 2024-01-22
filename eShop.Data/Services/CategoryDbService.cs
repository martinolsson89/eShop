using AutoMapper;
using eShop.Data.Contexts;
using eShop.Data.Entities;

namespace eShop.Data.Services;

public class CategoryDbService(EShopContext db, IMapper mapper) : DbService(db, mapper)
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        //IncludeNavigationsFor<Filter>();
        //IncludeNavigationsFor<Product>();
        var result = await base.GetAsync<TEntity, TDto>();
        return result;
    }
}