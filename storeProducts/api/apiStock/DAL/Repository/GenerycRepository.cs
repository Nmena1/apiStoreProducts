using apiStock.DAL.Context;
using apiStock.DAL.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace apiStock.DAL.Repository
{
    public class GenerycRepository<TModel> : IGenerycRepository<TModel> where TModel : class
    {
        private readonly dbContext _dbContext;

        public GenerycRepository(dbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        
        public async Task<TModel> get(Expression<Func<TModel, bool>> expression)
        {
            try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(expression);
                return model;
            }
            catch { throw; }
        }

        public async Task<TModel> create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();

                return model;
            }
            catch { throw; }
        }

        public async Task<bool> update(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch { throw; }
        }

        public async Task<bool> delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model); 
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch { throw; }
        }

        public async Task<IQueryable<TModel>> consult(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch { throw; }
        }

    }
}
