using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;
using SMLIB.View;

namespace SMLIB.Repository
{
    public class OrderRepo
    {
        public static void insertOrder(Guid orderId, Guid transactionId, Guid customer, Guid productId,double orderQuantity)
        {
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                var order = new Order()
                {
                    OrderId = orderId,               
                    Order_DateTime = DateTime.Now,
                    Order_ProductId = productId,
                    Order_Transaction = transactionId,
                    Order_UserId = customer,
                    Order_Quantity = orderQuantity
                 
                };
                context.Orders.Add(order);
                context.SaveChanges();
            }

        }

        public static void updateOrderQuantity(Guid transactionId,Guid productId,double quantity) {         
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                var orders = (from order in context.Orders
                              where order.Order_Transaction == transactionId && order.Order_ProductId == productId
                              select order).FirstOrDefault();

                orders.Order_Quantity = quantity;
                context.SaveChanges();

            }
        }
    }
}
