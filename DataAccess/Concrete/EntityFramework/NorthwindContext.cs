using Core.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Data tabloları ile proje classlarını baglamak
    public class NorthwindContext:DbContext
    {
        //Hangi data ile kullanacagını gosterdigin yer.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Normal bir projede buraya server= Ip adresi yazılır.
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
            optionsBuilder.UseSqlServer(@"Server=(localdb)\projectsV13;Database=Northwind;Trusted_Connection=true");
        }
        //Neyin neye karsılık gelecegini yazdık.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
