using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyWeb.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContextApp _db;
    internal DbSet<T> dbSet;

    public Repository(DbContextApp db)
    {
        _db= db;
        this.dbSet=_db.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query=query.Where(filter);

        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return [.. query];
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRage(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
