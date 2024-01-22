using AutoMapper;
using eShop.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data.Services;

public class DbService : IDbService
{
    protected readonly EShopContext _db;
    private protected readonly IMapper _mapper;
    public DbService(EShopContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public virtual async Task<TDto> SingleAsync<TEntity, TDto>(int id) where TEntity : class, IEntity where TDto : class
    {
        var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        return _mapper.Map<TDto>(entity);
    }

    public void Update<TEntity, TDto>(TDto dto) where TEntity : class, IEntity where TDto : class
    {
        // Note that this method isn't asynchronous because Update modifies
        // an already exisiting object in memory, which is very fast.
        var entity = _mapper.Map<TEntity>(dto);
        _db.Set<TEntity>().Update(entity);
    }
    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try
        {
            var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { return false; }

        return true;
    }
    public bool Delete<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { return false; }

        return true;
    }

    public virtual async Task<List<TDto>> GetAsync<TEntity, TDto>() 
        where TEntity : class where TDto : class
    {
        //IncludeNavigationsFor<TEntity>();
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }
    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }
    public async Task<bool> SaveChangesAsync() => await _db.SaveChangesAsync() >= 0;
}