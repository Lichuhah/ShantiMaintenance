using Assets.Infrastructure.Requests;
using Microsoft.EntityFrameworkCore;

namespace Assets.Base;

public class BaseRepository<TEntity> where TEntity : BaseEntity
{
    protected DbContext _context;
    protected DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> GetDbSet()
    {
        return _dbSet;
    }

    public virtual int TotalCount()
    {
        return GetDbSet().Count();
    }
    
    public virtual IEnumerable<TEntity> All(AllRequestOptions? options = null)
    {
        IQueryable<TEntity> queryable = GetDbSet().OrderBy(x=>x.Id);
        if (options != null)
        {
            if (options.Page != null && options.Limit != null)
            {
                int skip = ((options.Page - 1) * options.Limit) ?? 0;
                queryable = queryable.Skip(skip).Take((int)options.Limit);
            }
        }

        return queryable.AsNoTracking().ToList();
    }

    public virtual IEnumerable<TEntity> All(Func<TEntity, bool> predicate)
    {
        return All().Where(predicate).ToList();
    }

    public virtual TEntity? Get(int id)
    {
        return All().FirstOrDefault(x => x.Id == id);
    }

    public virtual void Save(TEntity item)
    {
        try
        {
            if (item.Id > 0) _context.Entry(item).State = EntityState.Modified;
            else _dbSet.Add(item);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException?.Message);
        }
    }

    public virtual void Delete(TEntity item)
    {
        _dbSet.Remove(item);
        _context.SaveChanges();
    }
}