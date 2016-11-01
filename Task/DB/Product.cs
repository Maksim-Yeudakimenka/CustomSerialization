using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    public partial class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        private Product(SerializationInfo info, StreamingContext context)
        {
            ProductID = info.GetInt32("ProductID");
            ProductName = info.GetString("ProductName");
            SupplierID = info.GetInt32("SupplierID");
            CategoryID = info.GetInt32("CategoryID");
            QuantityPerUnit = info.GetString("QuantityPerUnit");
            UnitPrice = info.GetDecimal("UnitPrice");
            UnitsInStock = info.GetInt16("UnitsInStock");
            UnitsOnOrder = info.GetInt16("UnitsOnOrder");
            ReorderLevel = info.GetInt16("ReorderLevel");
            Discontinued = info.GetBoolean("Discontinued");
            Category = (Category) info.GetValue("Category", typeof(Category));
            Order_Details = (ICollection<Order_Detail>) info.GetValue("Order_Details", typeof(ICollection<Order_Detail>));
            Supplier = (Supplier) info.GetValue("Supplier", typeof(Supplier));
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProductID", ProductID);
            info.AddValue("ProductName", ProductName);
            info.AddValue("SupplierID", SupplierID);
            info.AddValue("CategoryID", CategoryID);
            info.AddValue("QuantityPerUnit", QuantityPerUnit);
            info.AddValue("UnitPrice", UnitPrice);
            info.AddValue("UnitsInStock", UnitsInStock);
            info.AddValue("UnitsOnOrder", UnitsOnOrder);
            info.AddValue("ReorderLevel", ReorderLevel);
            info.AddValue("Discontinued", Discontinued);

            var dbContext = context.Context as DbContext;
            if (dbContext != null)
            {
                var objectContext = ((IObjectContextAdapter) dbContext).ObjectContext;
                objectContext.LoadProperty(this, p => p.Category);
                objectContext.LoadProperty(this, p => p.Order_Details);
                objectContext.LoadProperty(this, p => p.Supplier);

                info.AddValue("Category", Category, typeof(Category));
                info.AddValue("Order_Details", Order_Details, typeof(ICollection<Order_Detail>));
                info.AddValue("Supplier", Supplier, typeof(Supplier));
            }
        }
    }
}
