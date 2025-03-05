using System.Linq.Expressions;

namespace apiStock.DAL.Repository.Contract
{
    public interface IGenerycRepository<TModel> where TModel : class
    {
        //write all methos for working with all our models
        Task<TModel> get(Expression<Func<TModel, bool>> expression);

        Task<TModel> create(TModel model);

        Task<bool> update(TModel model);

        Task<bool> delete(TModel model);

        Task<IQueryable<TModel>> consult(Expression<Func<TModel, bool>> filtr = null);


    }
}
