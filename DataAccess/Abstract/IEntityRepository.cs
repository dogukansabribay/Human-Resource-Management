using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //Generic Consraint
    //class : refereans tip
    //new() : newlenebilir
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null); 
        List<T> GetAll();
        T Get(Expression<Func<T, bool>> filter); 
        T GetById(int id);
        void Add(T entity);
        void Add(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> filter);

        bool Any(Expression<Func<T, bool>> filter);
    }
}
