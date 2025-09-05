using Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Interfaces
{
	public interface IRepository<T> where T : class
	{
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int entityId);

        //Task<T> GetAsync(Expression<Func<T, bool>> expression,
        //                     string[] includes = null);
        //Task<IEnumerable<T>> GetAllAsync(string[] includes = null!,
        //                                    Expression<Func<T, bool>> expression = null!,
        //                                    IOrderedEnumerable<T> orderBy = null);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
                                            
                                             
        Task<T> GetAsync(Expression<Func<T, bool>> expression,params Expression<Func<T, object>>[] includes);


    }
}
