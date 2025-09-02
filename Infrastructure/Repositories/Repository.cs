using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext context;
        protected readonly DbSet<T> _entities;

        public Repository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = this.context.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);

            //SaveChangesAsync();

        }


        public async Task DeleteAsync(int entityId)
        {
            var result = await _entities.FindAsync(entityId);
            _entities.Remove(result!);
            //SaveChangesAsync();
        }




        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null,
                                            Expression<Func<T, bool>> expression = null,
                                            IOrderedEnumerable<T> orderBy = null)
        {
            IQueryable<T> query = _entities;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            if (orderBy != null)
                query = orderBy.AsQueryable();
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null!)
        {
            IQueryable<T> query = _entities;
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.FirstOrDefaultAsync(expression);
        }

        public  Task UpdateAsync(T entity)
        {
            _entities.Attach(entity);
            _entities.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }


        private int SaveChangesAsync() => context.SaveChanges();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[] includes = null,
                                                Expression<Func<T, bool>> expression = null!,
                                                IOrderedEnumerable<T> orderBy = null)
        {
            IQueryable<T> query = _entities;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            if (orderBy != null)
                query = orderBy.AsQueryable();
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression,  Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _entities;
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.FirstOrDefaultAsync(expression);
        }

        
    }
}
