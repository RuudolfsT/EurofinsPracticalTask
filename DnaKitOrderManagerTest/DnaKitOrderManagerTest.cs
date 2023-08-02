using EurofinsHomework;

namespace DnaKitOrderManagerTest
{
    [TestClass]
    public class DnaKitOrderManagerTest
    {
        [TestMethod]
        [DataRow(false, 0, "Order failed as delivery date was set in past")]
        [DataRow(true, 1, "Order went through as delivery date was set in the future")]
        public void CreateOrderValidateDeliveryDateTest(bool isDeliveryDateCorrect, int expectedOrderCount, string errorMessage)
        {
            var deliveryDate = isDeliveryDateCorrect ? DateTime.Now.AddDays(10) : DateTime.MinValue;
            var orderManager = new DnaKitOrderManager();
            var kit = new DnaKit();
            kit.KitName = "TEST1";

            orderManager.CreateOrder(1, kit, 5, deliveryDate);
            var doneOrders = orderManager.GetCustomerOrders().Count;

            Assert.AreEqual(expectedOrderCount, doneOrders, errorMessage);
        }

        [TestMethod]
        [DataRow(-1, 0, "Order failed as amount was negative")]
        [DataRow(1000, 0, "Order failed as amount was too large")]
        [DataRow(10, 1, "Order went through as amount was set correctly")]
        public void CreateOrderValidateAmountTest(int orderAmount, int expectedOrderCount, string errorMessage)
        {
            var amount = orderAmount;
            var orderManager = new DnaKitOrderManager();

            var kit = new DnaKit();
            kit.KitName = "TEST1";

            orderManager.CreateOrder(1, kit, amount, DateTime.Now.AddDays(10));
            var doneOrders = orderManager.GetCustomerOrders().Count;

            Assert.AreEqual(expectedOrderCount, doneOrders, errorMessage);
        }

        [TestMethod]
        [DataRow(false, 0, "Order failed as a kit was not selected")]
        [DataRow(true, 1, "Order went through as a kit was selected")]
        public void CreateOrderValidateKitSelectedTest(bool isKitSelected, int expectedOrderCount, string errorMessage)
        {
            var kit = new DnaKit();
            kit.KitName = "TEST1";

            var selectedKit = isKitSelected ? kit : null;
            var orderManager = new DnaKitOrderManager();

            orderManager.CreateOrder(1, selectedKit, 10, DateTime.Now.AddDays(10));
            var doneOrders = orderManager.GetCustomerOrders().Count;

            Assert.AreEqual(expectedOrderCount, doneOrders, errorMessage);
        }

        [TestMethod]
        [DataRow(5, 50, "No discount should be applied")]
        [DataRow(10, 95, "5% discount should be applied")]
        [DataRow(100, 850, "15% discount should be applied")]

        public void CalculateOrderTotalPriceWithDiscount(int orderAmount, int expectedPrice, string errorMessage)
        {
            var kit = new DnaKit();
            kit.KitName = "TEST1";
            kit.Price = 10M; // kit price changed for simplicity
            var orderManager = new DnaKitOrderManager();

            orderManager.CreateOrder(1, kit, orderAmount, DateTime.Now.AddDays(10));
            var order = orderManager.GetCustomerOrders().First();
            var orderPrice = order.Price;

            Assert.AreEqual(expectedPrice, orderPrice, errorMessage);
        }

        [TestMethod]
        public void GetCustomerOrdersTest()
        {
            var kit = new DnaKit();
            kit.KitName = "TEST1";
            kit.Price = 10M; // kit price changed for simplicity

            var orderManager = new DnaKitOrderManager();
            var order1 = new Order()
            {
                Amount = 5,
                Kit = kit,
                DeliveryDate = DateTime.Now.AddDays(10),
                CustomerId = 1
            };

            var order2 = new Order()
            {
                Amount = 100,
                Kit = kit,
                DeliveryDate = DateTime.Now.AddDays(10),
                CustomerId = 2
            };

            orderManager.CreateOrder(order1.CustomerId, order1.Kit, order1.Amount, order1.DeliveryDate);
            orderManager.CreateOrder(order2.CustomerId, order2.Kit, order2.Amount, order2.DeliveryDate);

            var orderCount = orderManager.GetCustomerOrders().Count;

            Assert.AreEqual(2, orderCount, "Order lists should be equal");
        }

        [TestMethod]
        public void GetCustomerOrderPriceSumTest()
        {
            var kit = new DnaKit();
            kit.KitName = "TEST1";
            kit.Price = 10M; // kit price changed for simplicity

            var orderManager = new DnaKitOrderManager();
            var order1 = new Order()
            {
                Amount = 5,
                Kit = kit,
                DeliveryDate = DateTime.Now.AddDays(10),
                CustomerId = 1
            };

            var order2 = new Order()
            {
                Amount = 100,
                Kit = kit,
                DeliveryDate = DateTime.Now.AddDays(10),
                CustomerId = 2
            };

            orderManager.CreateOrder(order1.CustomerId, order1.Kit, order1.Amount, order1.DeliveryDate);
            orderManager.CreateOrder(order2.CustomerId, order2.Kit, order2.Amount, order2.DeliveryDate);

            var totalSum = orderManager.GetOrderPriceSum();

            Assert.AreEqual(900, totalSum, "Order prices should be equal");
        }

        [TestMethod]
        public void GetCustomerOrderInfoTest()
        {
            var kit = new DnaKit();
            kit.KitName = "TEST1";
            kit.Price = 10M; // kit price changed for simplicity

            var orderManager = new DnaKitOrderManager();

            var amount = 5;
            var deliveryDate = DateTime.Now.AddDays(10);
            var customerId = 1;
            var order1 = new Order()
            {
                Amount = amount,
                Kit = kit,
                DeliveryDate = deliveryDate,
                CustomerId = customerId
            };

            var expectedOrderInfo =
                    $"{nameof(order1.CustomerId)}: {customerId}\n" +
                    $"{nameof(order1.Kit.KitName)}: {kit.KitName}\n" +
                    $"{nameof(order1.Amount)}: {amount}\n" +
                    $"{nameof(order1.Price)}: {50}";

            orderManager.CreateOrder(order1.CustomerId, order1.Kit, order1.Amount, order1.DeliveryDate);

            var orders = orderManager.GetCustomerOrders();
            foreach (var order in orders)
            {
                var orderInfo = order.GetOrderInfo();
                Assert.AreEqual(expectedOrderInfo, orderInfo);
            }
        }
    }
}
