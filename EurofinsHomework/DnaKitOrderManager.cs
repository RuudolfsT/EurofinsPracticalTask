namespace EurofinsHomework
{
    public class DnaKitOrderManager
    {
        private List<Order> orders = new List<Order>();

        public List<Order> GetCustomerOrders()
        {
            return orders;
        }

        public decimal GetOrderPriceSum()
        {
            var sum = orders.Sum(x => x.Price);
            return sum;
        }

        public void CreateOrder(int customerId, DnaKit kit, int amount, DateTime deliveryDate)
        {
            Order newOrder = new Order
            {
                CustomerId = customerId,
                Kit = kit,
                Amount = amount,
                DeliveryDate = deliveryDate
            };

            try
            {
                ValidateOrder(newOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(Resources.CustomerOrderFailed, customerId, ex.Message));
                return;
            }

            newOrder.Price = newOrder.CalculatePrice();
            orders.Add(newOrder);
        }

        public void ValidateOrder(Order order)
        {
            if (order.Kit == null)
            {
                throw new Exception(Resources.KitNotSelectedException);
            }

            if (order.Amount <= 0 || order.Amount > 999)
            {
                throw new Exception(Resources.AmountException);
            }

            if (order.DeliveryDate < DateTime.Today)
            {
                throw new Exception(Resources.DateSetInPastException);
            }
        }
    }
}