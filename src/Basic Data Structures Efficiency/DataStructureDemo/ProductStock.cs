namespace BasicDataStructureDemo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductStock : IProductStock
    {
        private readonly List<Product> products;
        private readonly Dictionary<string, Product> productsByLabels;
        private readonly Dictionary<int, HashSet<Product>> productsByQuantity;
        private readonly SortedDictionary<decimal, HashSet<Product>> productsByPrice;

        public ProductStock()
        {
            this.products = new List<Product>();
            this.productsByLabels = new Dictionary<string, Product>();
            this.productsByQuantity = new Dictionary<int, HashSet<Product>>();
            this.productsByPrice = new SortedDictionary<decimal, HashSet<Product>>(
                Comparer<decimal>.Create((first, second) => second.CompareTo(first)));
        }

        public int Count => this.products.Count;

        public bool Contains(Product product)
        {
            this.ValidateProduct(product);

            return this.productsByLabels.ContainsKey(product.Label);
        }

        public void Add(Product product)
        {
            this.ValidateProduct(product);

            if (this.productsByLabels.ContainsKey(product.Label))
            {
                throw new InvalidOperationException("Product with the same label already exists.");
            }

            this.products.Add(product);
            this.productsByLabels[product.Label] = product;

            if (!this.productsByQuantity.ContainsKey(product.Quantity))
            {
                this.productsByQuantity[product.Quantity] = new HashSet<Product>();
            }

            if (!this.productsByPrice.ContainsKey(product.Price))
            {
                this.productsByPrice[product.Price] = new HashSet<Product>();
            }

            this.productsByQuantity[product.Quantity].Add(product);
            this.productsByPrice[product.Price].Add(product);
        }

        public bool Remove(Product product)
        {
            this.ValidateProduct(product);

            if (!this.productsByLabels.ContainsKey(product.Label))
            {
                return false;
            }

            this.products.Remove(product);
            this.productsByLabels.Remove(product.Label);

            this.productsByPrice[product.Price].Remove(product);
            this.productsByQuantity[product.Quantity].Remove(product);

            return true;
        }

        public Product Find(int index)
        {
            if (index < 0 || index >= this.products.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            return this.products[index];
        }

        public Product FindByLabel(string label)
        {
            if (!this.productsByLabels.ContainsKey(label))
            {
                throw new InvalidOperationException("Product could not be found.");
            }

            return this.productsByLabels[label];
        }

        public Product FindMostExpensiveProduct()
        {
            if (!this.productsByPrice.Any())
            {
                throw new InvalidOperationException("No products found.");
            }

            var mostExpensiveProducts = this.productsByPrice.First().Value;

            return mostExpensiveProducts.Last();
        }

        public IEnumerable<Product> FindAllByPrice(decimal price)
        {
            if (!this.productsByPrice.ContainsKey(price))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByPrice[price];
        }

        public IEnumerable<Product> FindAllByQuantity(int quantity)
        {
            if (!this.productsByQuantity.ContainsKey(quantity))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByQuantity[quantity];
        }

        public Product this[int index] => this.Find(index);

        public IEnumerator<Product> GetEnumerator() => this.products.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void ValidateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException("Product cannot be null");
            }
        }
    }
}
