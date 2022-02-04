namespace BasicDataStructureDemo
{
    using System.Collections.Generic;

    public interface IProductStock : IEnumerable<Product>
    {
        int Count { get; }

        Product this[int index] { get; }

        bool Contains(Product product);

        void Add(Product product);

        bool Remove(Product product);

        Product Find(int index);

        Product FindByLabel(string label);

        Product FindMostExpensiveProduct();

        IEnumerable<Product> FindAllByPrice(decimal price);

        IEnumerable<Product> FindAllByQuantity(int quantity);
    }
}
