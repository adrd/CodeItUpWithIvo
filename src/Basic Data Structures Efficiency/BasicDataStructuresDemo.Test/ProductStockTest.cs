namespace BasicDataStructuresDemo.Test
{
    using System.Linq;
    using BasicDataStructureDemo;
    using Xunit;

    public class ProductStockTest
    {
        private readonly ProductStock productStock;
        private readonly Product product;

        public ProductStockTest()
        {
            this.productStock = new ProductStock();
            this.product = new Product("Test", 10, 200);

            this.productStock.Add(this.product);
        }

        [Fact]
        public void AddShouldAddTheProduct()
        {
            // Assert
            var productByIndex = this.productStock.Find(0);
            var productByQuantity = this.productStock.FindAllByQuantity(200).FirstOrDefault();
            var productByPrice = this.productStock.FindAllByPrice(10).FirstOrDefault();

            Assert.Equal(this.product.Label, productByIndex.Label);
            Assert.True(this.productStock.Contains(product));
            Assert.NotNull(productByQuantity);
            Assert.Equal(this.product.Label, productByQuantity.Label);
            Assert.NotNull(productByPrice);
            Assert.Equal(this.product.Label, productByPrice.Label);
        }

        [Fact]
        public void ContainsShouldReturnTrueWithValidProduct()
        {
            // Act
            var contains = this.productStock.Contains(this.product);

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void ContainsShouldReturnFalseWithInvalidProduct()
        {
            // Act
            var contains = this.productStock.Contains(new Product("Another", 20, 100));

            // Assert
            Assert.False(contains);
        }

        [Fact]
        public void FindMostExpensiveProductShouldReturnCorrectProduct()
        {
            // Arrange
            this.AddProducts();

            // Act
            var result = this.productStock.FindMostExpensiveProduct();

            // Assert
            Assert.Equal(1200, result.Price);
        }

        [Fact]
        public void FindAllByPriceShouldReturnCorrectCollection()
        {
            // Arrange
            this.AddProducts();

            // Act
            var result = this.productStock.FindAllByPrice(800);

            // Assert
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void RemoveShouldDeleteTheProductFromTheStock()
        {
            // Arrange
            this.AddProducts();

            // Act
            var result = this.productStock.Remove(this.product);

            // Assert
            var productsByPrice = this.productStock.FindAllByPrice(10);
            var productsByQuantity = this.productStock.FindAllByQuantity(200);

            Assert.True(result);
            Assert.Equal(7, this.productStock.Count);
            Assert.False(this.productStock.Contains(this.product));
            Assert.Empty(productsByPrice);
            Assert.Empty(productsByQuantity);
        }

        private void AddProducts()
        {
            this.productStock.Add(new Product("Another", 200, 20));
            this.productStock.Add(new Product("Yet Another", 1200, 10));
            this.productStock.Add(new Product("Even Yet Another", 300, 50));
            this.productStock.Add(new Product("Even Yet Yet Another", 800, 80));
            this.productStock.Add(new Product("Even Yet Yet Another 2", 800, 90));
            this.productStock.Add(new Product("Even Yet Yet Another 3", 800, 70));
            this.productStock.Add(new Product("Even Yet Yet Another 4", 800, 180));
        }
    }
}
