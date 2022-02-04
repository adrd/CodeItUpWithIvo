namespace RedBlackTree.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class CorrectnessTests
    {
        [Test]
        public void Insert_SingleElement_ShouldIncreaseCount()
        {
            var rbt = new RedBlackTree<int>();
            rbt.Insert(5);

            Assert.AreEqual(1, rbt.Count());
        }

        [Test]
        public void Insert_MultipleElements_ShouldBeInsertedCorrectly()
        {
            var rbt = new RedBlackTree<int>();
            rbt.Insert(5);
            rbt.Insert(12);
            rbt.Insert(18);
            rbt.Insert(37);
            rbt.Insert(48);
            rbt.Insert(60);
            rbt.Insert(80);

            var nodes = new List<int>();
            rbt.EachInOrder(nodes.Add);

            // Assert
            var expectedNodes = new int[] { 5, 12, 18, 37, 48, 60, 80 };

            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [Test]
        public void Insert_MultipleElements_ShouldBeBalanced()
        {
            var rbt = new RedBlackTree<int>();
            rbt.Insert(5);
            rbt.Insert(12);
            rbt.Insert(18);
            rbt.Insert(37);
            rbt.Insert(48);
            rbt.Insert(60);
            rbt.Insert(80);

            Assert.AreEqual(3, rbt.Search(12).Count());
            Assert.AreEqual(3, rbt.Search(60).Count());
        }
    }
}
