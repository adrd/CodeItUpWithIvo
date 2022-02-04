namespace ExtensionMethods.Enums
{
    using System;

    public static class OrderStatusExtensions
    {
        public static string GetMessage(this OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Pending:
                    return "Your order is pending.";
                case OrderStatus.Paid:
                    return "Your order is paid and is waiting shipment.";
                case OrderStatus.Shipped:
                    return "Your order is shipped and is awaiting delivery.";
                case OrderStatus.Completed:
                    return "Your order is delivered. Please leave a review on our site!";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), "Invalid order status value.");
            }
        }
    }
}
