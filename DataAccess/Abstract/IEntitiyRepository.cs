using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess
{
    //Bana istedigim tipi ver dedik. T tipi ne verirsen o. Ancak biz sadece entitiestekilerin gelebilmesini istiyorum. 
    //Class=referans tip olabilir demek. IEntity demek ise referans IEntity olucak demek. 
    //Ama biz burada product customer category cagırabildigimiz gibi Ientity yi de cagırabiliriz.
    //Bu yuzden new() ekledik. Yani vericegimiz T newlenebilir olmalı dedik.
    public interface IEntitiyRepository<T> where T:class,IEntity,new()
    {
        //Ilk ikiye flitreleme atadık. Bu kotu bir yazım bir daha kullanmıcaksın ancak simdi bir yaz.
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAllByCategory(int categoryId);
    }
}
