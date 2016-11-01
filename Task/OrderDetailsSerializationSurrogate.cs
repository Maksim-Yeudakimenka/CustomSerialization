using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    public class OrderDetailsSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail) obj;

            info.AddValue("OrderID", orderDetail.OrderID);
            info.AddValue("ProductID", orderDetail.ProductID);
            info.AddValue("UnitPrice", orderDetail.UnitPrice);
            info.AddValue("Quantity", orderDetail.Quantity);
            info.AddValue("Discount", orderDetail.Discount);

            var dbContext = context.Context as DbContext;
            if (dbContext != null)
            {
                var objectContext = ((IObjectContextAdapter) dbContext).ObjectContext;
                objectContext.LoadProperty(orderDetail, d => d.Order);
                objectContext.LoadProperty(orderDetail, d => d.Product);

                info.AddValue("Order", orderDetail.Order, typeof(Order));
                info.AddValue("Product", orderDetail.Product, typeof(Product));
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail) obj;

            orderDetail.OrderID = info.GetInt32("OrderID");
            orderDetail.ProductID = info.GetInt32("ProductID");
            orderDetail.UnitPrice = info.GetDecimal("UnitPrice");
            orderDetail.Quantity = info.GetInt16("Quantity");
            orderDetail.Discount = info.GetSingle("Discount");
            orderDetail.Order = (Order) info.GetValue("Order", typeof(Order));
            orderDetail.Product = (Product) info.GetValue("Product", typeof(Product));

            return orderDetail;
        }
    }
}