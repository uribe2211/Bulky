﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyWeb.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContextApp _db;
    internal DbSet<T> dbSet;
    private static readonly char[] separator = [','];

    public Repository(DbContextApp db)
    {
        _db= db;
        this.dbSet=_db.Set<T>();
        _db.Products.Include(u=>u.Category).Include(u=>u.CategoryId);
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        query=query.Where(filter);

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var item in includeProperties
                .Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
        }

        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(string? includeProperties=null)
    {
        IQueryable<T> query = dbSet;

        if(!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var item in includeProperties
                .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query=query.Include(item);
            }
        }

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
