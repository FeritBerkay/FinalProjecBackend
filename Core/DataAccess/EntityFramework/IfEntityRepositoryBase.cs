using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    //evrensel bir kod. Entitiy framework ile bir repository Basesesi. entitiy olan bir T ve context olan bir T atadık.
    //Tum operasyonları bir daha bir daha yazmayalım diye.
    public class IfEntityRepositoryBase<TEntity,TContext> :IEntityRepository<TEntity>
        where TEntity:class,IEntity,new() 
        where TContext:DbContext,new()
    {
        //Normalde product ve northwind olan yerler TEntitiy ve TContext yaptık. Yani ne atarsak onu implemente etmis olucagız. 
        public void Add(TEntity entity)
        {
            //Bellegi temizlemeye yara.
            using (TContext context = new TContext())
            {
                //Ilk basta referansı al
                //Bu eklenecek bir nesne ".Added"
                //Ekle. 
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                //Flitrene gore getir.
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //Filter nullsa ilki degilsse ikincisi.
                //Ilki select * from product ı calıstırır.
                //Ikıncısı ise flitreli halini
                return filter == null ?
                    context.Set<TEntity>().ToList() :
                    context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public List<TEntity> GetAllByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
