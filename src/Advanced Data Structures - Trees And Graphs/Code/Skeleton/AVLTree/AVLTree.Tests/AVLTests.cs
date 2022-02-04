namespace AVLTree.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AVLTests
    {
        [TestMethod]
        public void TraverseInOrder_AfterSingleInsert()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(1);

            // Act
            var nodes = new List<int>();
            avl.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 1 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void TraverseInOrder_AfterMultipleInserts()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(2);
            avl.Insert(1);
            avl.Insert(3);

            // Act
            var nodes = new List<int>();
            avl.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 1, 2, 3 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void Contains_ExistingElement_ShouldReturnTrue()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(2);
            avl.Insert(1);
            avl.Insert(3);

            // Act
            // Assert
            Assert.IsTrue(avl.Contains(1));
            Assert.IsTrue(avl.Contains(2));
            Assert.IsTrue(avl.Contains(3));
        }

        [TestMethod]
        public void Contains_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(2);
            avl.Insert(1);
            avl.Insert(3);

            // Act
            var contains = avl.Contains(5);

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Height_RootRight()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(1);
            avl.Insert(2);

            // Act
            // Assert
            Assert.AreEqual(2, avl.Root.Height);
            Assert.AreEqual(1, avl.Root.Right.Height);
        }

        [TestMethod]
        public void Height_RootLeft()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(2);
            avl.Insert(1);

            // Act
            // Assert
            Assert.AreEqual(2, avl.Root.Height);
            Assert.AreEqual(1, avl.Root.Left.Height);
        }

        [TestMethod]
        public void Height_RootLeftRight()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(2);
            avl.Insert(1);
            avl.Insert(3);

            // Act
            // Assert
            Assert.AreEqual(2, avl.Root.Height);
            Assert.AreEqual(1, avl.Root.Left.Height);
            Assert.AreEqual(1, avl.Root.Right.Height);
        }

        [TestMethod]
        public void Rebalance_RootShouldHaveHeightTwo()
        {
            // Arrange
            var avl = new AVL<int>();
            avl.Insert(1);
            avl.Insert(2);
            avl.Insert(3);

            // Assert
            Assert.AreEqual(2, avl.Root.Height);
        }

        [TestMethod]
        public void Rebalance_TestHeightOneNodes()
        {
            // Arrange
            var avl = new AVL<int>();
            for (var i = 1; i < 10; i++)
            {
                avl.Insert(i);
            }

            // Assert
            Assert.AreEqual(1, avl.Root.Left.Left.Height); // 1
            Assert.AreEqual(1, avl.Root.Left.Right.Height); // 3
            Assert.AreEqual(1, avl.Root.Right.Left.Height); // 5
            Assert.AreEqual(1, avl.Root.Right.Right.Left.Height); // 7
            Assert.AreEqual(1, avl.Root.Right.Right.Right.Height); // 9
        }

        [TestMethod]
        public void Rebalance_TestHeightTwoNodes()
        {
            // Arrange
            var avl = new AVL<int>();
            for (var i = 1; i < 10; i++)
            {
                avl.Insert(i);
            }

            // Assert
            Assert.AreEqual(2, avl.Root.Left.Height); // 2
            Assert.AreEqual(2, avl.Root.Right.Right.Height); // 8
        }

        [TestMethod]
        public void Rebalance_TestHeightThreeNodes()
        {
            // Arrange
            var avl = new AVL<int>();
            for (var i = 1; i < 10; i++)
            {
                avl.Insert(i);
            }

            // Assert
            Assert.AreEqual(3, avl.Root.Right.Height); // 6
        }

        [TestMethod]
        public void Rebalance_TestHeightFourNodes()
        {
            // Arrange
            var avl = new AVL<int>();
            for (var i = 1; i < 10; i++)
            {
                avl.Insert(i);
            }

            // Assert
            Assert.AreEqual(4, avl.Root.Height); // 4
        }

        [TestMethod]
        public void Rebalance_SingleRight()
        {
            // Arrange
            var avl = new AVL<int>();

            // Act
            avl.Insert(3);
            avl.Insert(2);
            avl.Insert(1);

            // Assert
            Assert.AreEqual(2, avl.Root.Value);
        }

        [TestMethod]
        public void Rebalance_SingleLeft()
        {
            // Arrange
            var avl = new AVL<int>();

            // Act
            avl.Insert(1);
            avl.Insert(2);
            avl.Insert(3);

            // Assert
            Assert.AreEqual(2, avl.Root.Value);
        }

        [TestMethod]
        public void Rebalance_DoubleRight()
        {
            // Arrange
            var avl = new AVL<int>();

            // Act
            avl.Insert(5);
            avl.Insert(2);
            avl.Insert(4);

            // Assert
            Assert.AreEqual(4, avl.Root.Value);
            Assert.AreEqual(2, avl.Root.Height);
            Assert.AreEqual(1, avl.Root.Left.Height);
            Assert.AreEqual(1, avl.Root.Right.Height);
        }

        [TestMethod]
        public void Rebalance_DoubleLeft()
        {
            // Arrange
            var avl = new AVL<int>();

            // Act
            avl.Insert(5);
            avl.Insert(7);
            avl.Insert(6);

            // Assert
            Assert.AreEqual(6, avl.Root.Value);
            Assert.AreEqual(2, avl.Root.Height);
            Assert.AreEqual(1, avl.Root.Left.Height);
            Assert.AreEqual(1, avl.Root.Right.Height);
        }

        [TestMethod, Timeout(400)]
        public void Performance_Insert_Contains()
        {
            var avl = new AVL<int>();

            for (var i = 0; i < 100000; i++)
            {
                avl.Insert(i);
            }

            for (var i = 0; i < 100000; i++)
            {
                Assert.IsTrue(avl.Contains(i));
            }
        }
    }
}
