namespace Trees.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BSTTests
    {
        [TestMethod]
        public void Insert_Single_TraverseInOrder()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(1);

            // Act
            var nodes = new List<int>();
            bst.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 1 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void Insert_Multiple_TraverseInOrder()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(3);

            // Act
            var nodes = new List<int>();
            bst.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 1, 2, 3 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void Contains_ExistingElement_ShouldReturnTrue()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(3);

            // Act
            var contains = bst.Contains(1);

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(3);

            // Act
            var contains = bst.Contains(5);

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Insert_Multiple_DeleteMin_Should_Work_Correctly()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(3);

            // Act
            bst.DeleteMin();
            var nodes = new List<int>();
            bst.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 2, 3 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void DeleteMin_Empty_Tree_Should_Work_Correctly()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();

            // Act
            bst.DeleteMin();
            var nodes = new List<int>();
            bst.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void DeleteMin_One_Element_Should_Work_Correctly()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(1);

            // Act
            bst.DeleteMin();
            var nodes = new List<int>();
            bst.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void Search_NonExistingElement_ShouldReturnEmptyTree()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            // Act
            var result = bst.Search(5);     

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ExistingElement_ShouldReturnSubTree()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();

            bst.Insert(10);
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(1);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(9);
            bst.Insert(37);
            bst.Insert(39);
            bst.Insert(45);

            // Act
            var result = bst.Search(5);
            var nodes = new List<int>();
            result.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 1, 3, 4, 5, 8, 9};
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void Range_ExistingElements_ShouldReturnCorrectElements()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();

            bst.Insert(10);
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(1);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(9);
            bst.Insert(37);
            bst.Insert(39);
            bst.Insert(45);

            // Act
            var result = bst.Range(4, 37);

            // Assert
            var expectedNodes = new int[] { 4, 5, 8, 9, 10, 37 };
            CollectionAssert.AreEqual(expectedNodes, result.ToArray());
        }

        [TestMethod]
        public void Range_ExistingElements_ShouldReturnCorrectCount()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();

            bst.Insert(10);
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(1);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(9);
            bst.Insert(37);
            bst.Insert(39);
            bst.Insert(45);

            // Act
            var result = bst.Range(4, 37);

            // Assert
            var expectedNodes = new int[] { 4, 5, 8, 9, 10, 37 };
            Assert.AreEqual(expectedNodes.Length, result.ToArray().Length);
        }
    }
}
