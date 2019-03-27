using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace Data
{
    public class ContextManager<TEntity> where TEntity : class
    {

        public static void Add(TEntity entity)
        {
            using (var context = new TestGeorgeEntities())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public static List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TestGeorgeEntities())
            {
                var result = context.Set<TEntity>().AsQueryable();
                result = includes.Aggregate(result, (current, include) => current.Include(include));
                return result.ToList();
            }
        }

        public static List<TEntity> Where(Expression<Func<TEntity, bool>> whereClause, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TestGeorgeEntities())
            {
                var result = context.Set<TEntity>().Where(whereClause);
                result = includes.Aggregate(result, (current, include) => current.Include(include));
                return result.ToList();
            }
        }

        public static TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereClause, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TestGeorgeEntities())
            {
                var result = context.Set<TEntity>().AsQueryable();
                result = includes.Aggregate(result, (current, include) => current.Include(include));
                return result.SingleOrDefault(whereClause);

            }
        }

        public static TEntity SingleOrDefault(TestGeorgeEntities context, Expression<Func<TEntity, bool>> whereClause, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = context.Set<TEntity>().AsQueryable();
            result = includes.Aggregate(result, (current, include) => current.Include(include));
            return result.SingleOrDefault(whereClause);
        }


        public static void Delete(Expression<Func<TEntity, bool>> whereClause)
        {
            using (var context = new TestGeorgeEntities())
            {
                var entity = context.Set<TEntity>().SingleOrDefault(whereClause);
                if (entity != null)
                {
                    var result = context.Set<TEntity>().Remove(entity);
                    context.SaveChanges();
                }
            }
        }

        public static TEntity Update(TEntity toUpdate, Expression<Func<TEntity, bool>> whereClause)
        {
            using (var context = new TestGeorgeEntities())
            {
                var result = context.Set<TEntity>().SingleOrDefault(whereClause);
                context.Entry(result).CurrentValues.SetValues(toUpdate);
                context.SaveChanges();
                return toUpdate;
            }
        }
    }
}
