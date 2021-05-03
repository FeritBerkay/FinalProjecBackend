//Core evrensel katmandır. Sadece northwind de degil tum .net projelerinde kullanabiliriz. Core katmanı diger katmanları referans almaz.
//Alltaki using yok cunku biz buraya projeye baglamak istemiyoruz.
//using Entities.Abstract;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Bana istedigim tipi ver dedik. T tipi ne verirsen o. Ancak biz sadece entitiestekilerin gelebilmesini istiyorum. 
    //Class=referans tip olabilir demek. IEntity demek ise referans IEntity olucak demek. 
    //Ama biz burada product customer category cagırabildigimiz gibi Ientity yi de cagırabiliriz.
    //Bu yuzden new() ekledik. Yani vericegimiz T newlenebilir olmalı dedik.
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //Ilk ikiye flitreleme atadık. Bu kotu bir yazım bir daha kullanmıcaksın ancak simdi bir yaz.
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
