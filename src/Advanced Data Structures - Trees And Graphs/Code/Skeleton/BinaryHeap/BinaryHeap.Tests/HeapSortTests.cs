namespace BinaryHeap.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HeapSortTests
    {
        [TestMethod]
        public void Sort_SingleElement()
        {
            // Arrange
            var arr = new int[] { 5 };

            // Act
            Heap<int>.Sort(arr);

            // Assert
            var exp = new int[] { 5 };
            CollectionAssert.AreEqual(exp, arr);
        }

        [TestMethod]
        public void Sort_TwoElements()
        {
            // Arrange
            var arr = new int[] { 5, 1 };

            // Act
            Heap<int>.Sort(arr);

            // Assert
            var exp = new int[] { 1, 5 };
            CollectionAssert.AreEqual(exp, arr);
        }

        [TestMethod]
        public void Sort_MultipleElements()
        {
            // Arrange
            var arr = new int[1000];
            var element = arr.Length - 1;
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = element--;
            }

            // Act
            Heap<int>.Sort(arr);

            // Assert
            var exp = new int[1000];
            for (var i = 0; i < exp.Length; i++)
            {
                exp[i] = i;
            }

            CollectionAssert.AreEqual(exp, arr);
        }

        [TestMethod]
        public void Sort_NegativeElements()
        {
            // Arrange
            var arr = new int[] { 5, 1, -2 };

            // Act
            Heap<int>.Sort(arr);

            // Assert
            var exp = new int[] { -2, 1, 5 };
            CollectionAssert.AreEqual(exp, arr);
        }

        [TestMethod]
        public void Sort_EmptyArray()
        {
            // Arrange
            var arr = new int[] { };

            // Act
            Heap<int>.Sort(arr);

            // Assert
            var exp = new int[] { };
            CollectionAssert.AreEqual(exp, arr);
        }
    }
}
