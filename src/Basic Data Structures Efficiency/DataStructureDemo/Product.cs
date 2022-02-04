namespace BasicDataStructureDemo
{
    using System;

    public class Product
    {
        private string label; // Unique identifier.
        private decimal price;
        private int quantity;

        public Product(string label, decimal price, int quantity)
        {
            this.Label = label;
            this.Price = price;
            this.Quantity = quantity;
        }

        public string Label
        {
            get => this.label;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Label cannot be null or empty.");
                }

                this.label = value;
            }
        }

        public decimal Price
        {
            get => this.price;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be less than zero.");
                }

                this.price = value;
            }
        }

        public int Quantity
        {
            get => this.quantity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Quantity cannot be less than zero.");
                }

                this.quantity = value;
            }
        }

        public int CompareTo(Product other) => this.Price.CompareTo(other.Price);
    }
}
