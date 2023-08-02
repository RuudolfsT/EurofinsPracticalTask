using EurofinsHomework;

namespace DnsKitRun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DnaKit dnaKit = new DnaKit();
            dnaKit.KitName = "TEST";

            DnaKitOrderManager orderPlacement = new DnaKitOrderManager();
            orderPlacement.CreateOrder(1, dnaKit, 50, DateTime.Now.AddDays(10));
            orderPlacement.CreateOrder(2, dnaKit, 10, DateTime.Now.AddDays(-10));
            orderPlacement.CreateOrder(3, dnaKit, -5, DateTime.Now.AddDays(10));

            var allCustomerOrders = orderPlacement.GetCustomerOrders();
            foreach (var customerOrder in allCustomerOrders)
            {
                var orderInfo = customerOrder.GetOrderInfo();
                Console.Write(orderInfo);
            }
        }
    }
}