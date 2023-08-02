namespace EurofinsHomework
{
    public class Order
    {
        public int CustomerId { get; set; }
        public DnaKit Kit { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public decimal CalculatePrice()
        {
            decimal totalPrice;
            totalPrice = Kit.Price * Amount;

            if (Amount >= 10 && Amount < 50)
            {
                totalPrice -= totalPrice * 0.05M;
            }
            else if (Amount >= 50)
            {
                totalPrice -= totalPrice * 0.15M;
            }

            return Math.Round(totalPrice, 2);
        }

        public string GetOrderInfo()
        {
            string orderInfo = 
                $"{nameof(CustomerId)}: {CustomerId}\n" +
                $"{nameof(Kit.KitName)}: {Kit.KitName}\n" +
                $"{nameof(Amount)}: {Amount}\n" +
                $"{nameof(Price)}: {Price}";

            return orderInfo;
        }
    }
}
