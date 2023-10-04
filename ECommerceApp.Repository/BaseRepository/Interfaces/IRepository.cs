using System.Linq.Expressions;

namespace ECommerceApp.Repository.BaseRepository.Interfaces;

public interface IRepository<TEntity> where TEntity: class
{
    //Add
    Task<bool> AddAsync(TEntity Entity);
    Task<bool> DeleteAsync(TEntity Entity);
    Task<bool> DeleteById(object Id);
    //GetbyId
    TEntity GetById(object id);
    //GetAll
    IQueryable<TEntity> GetAll();
    //GetAllAsync
    Task<IQueryable<TEntity>> GetAllAsync();
    //GetbyPredicateAsync
    IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> filter);
    //SaveAsync
    Task<bool> SaveAsync();
    //UpdateAsync
    Task<bool> UpdateAsync(TEntity Entity);


}