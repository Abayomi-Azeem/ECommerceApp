using System.Linq.Expressions;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Enums;
using ECommerceApp.Repository.BaseRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repository.BaseRepository.Implementation;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    internal DbContext Context;
    internal DbSet<TEntity> Entity;

    public Repository(DbContext dbContext)
    {
        this.Context = dbContext;
        this.Entity = this.Context.Set<TEntity>();
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        await this.Entity.AddAsync(entity);     
        return await this.SaveAsync(); 
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        var dbEntityEntry = this.Context.Entry(entity);
        if (dbEntityEntry.State == EntityState.Detached)
        {
            this.Context.Set<TEntity>().Attach(entity);
        }
        dbEntityEntry.State = EntityState.Deleted;
        return await this.SaveAsync(); 
    }

    public async Task<bool> DeleteById(object Id)
    {
        var entity = this.GetById(Id);
        return await this.DeleteAsync(entity);
    }

    public TEntity GetById(object id)
    {
        var result = this.Entity.Find(id);
        return result;
    }

    public IQueryable<TEntity> GetAll()
    { 
        return this.Entity.AsQueryable();
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
       return await Task.FromResult(this.Entity.AsQueryable());
    }

    public  IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> filter)
    {
        var result = this.Entity.Where(filter);
        return result;
    }

    public async Task<bool> SaveAsync()
    {
        var changes = await this.Context.SaveChangesAsync();
        if(changes > 0) return true;
        return false;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var changes = this.Context.Update(entity);
        return await this.SaveAsync();
    }

    
}