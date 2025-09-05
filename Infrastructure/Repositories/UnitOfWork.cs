using Application.Interfaces;
using Application.Interfaces.Authentication;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Authentication;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public ICategory CategoryRepo { get; }

        public IProduct ProductRepo { get; }

        public IPhoto photoRepo { get; }

        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        private readonly AppDbContext context;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> repositories;
        //private IUserepo _users = null;
        public UnitOfWork(AppDbContext context , IMapper mapper , IImageManagementService imageManagementService)
        {
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
            CategoryRepo = new CategoryRepo(context);
            ProductRepo = new ProductRepo(context, mapper,imageManagementService);
            photoRepo = new PhotoRepo(context);
        }

        //public IUserepo Users => _users ?? new UserRepo(context);
         

        public void Dispose()
        {
            //context.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed) 
            { 
                if(disposing)
                {
                    context.Dispose();
                }
            } 
            this.disposed = true;
        }
      

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (repositories.ContainsKey(typeof(T)))
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            var repository = new Repository<T>(context);
            repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync(); 
      

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch 
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {

                await _transaction.DisposeAsync();
                _transaction = null!;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction=null!;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }
    }
}
