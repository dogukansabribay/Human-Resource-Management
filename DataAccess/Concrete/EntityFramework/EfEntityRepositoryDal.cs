using DataAccess.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryDal<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        private readonly KolayIkContext context;
        //private DbSet<T> dbSet;
        public EfEntityRepositoryDal(KolayIkContext context)
        {
            this.context = context;
            //dbSet = this.context.Set<T>();
        }
        public void Add(T entity)
        {
            //IDiposable pattern implementation of c#
            //dbSet.Add(entity);
            //var addedEntity = context.Entry(entity);
            //addedEntity.State = EntityState.Added;
            //context.SaveChanges();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Add(List<T> entities)
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Any(filter);
        }

        public void Delete(T entity)
        {
            //var deletedEntity = context.Entry(entity);
            //deletedEntity.State = EntityState.Deleted;
            //context.SaveChanges();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {                   
                    context.Set<T>().Remove(entity);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetAll(filter);
                    

                    foreach (var item in collection)
                    {
                        Delete(item);
                    }

                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter).FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return context.Set<T>().Where(filter).ToList();
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            //var updatedEntity = context.Entry(entity);
            //updatedEntity.State = EntityState.Modified;
            //context.SaveChanges();
            try
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
