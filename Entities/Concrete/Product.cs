using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Public yapıldıgında classa diger katmanlarda ulsabilsin demek.
    //Internal sadece Entities erisebilir demek business falan filan eresimez.
    public class Product:IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        //Short int in bir kucugu
        public short UnitsInStock { get; set; }
        //Decimal ondalıklı sayı
        public decimal UnitPrice { get; set; }

    }
}
