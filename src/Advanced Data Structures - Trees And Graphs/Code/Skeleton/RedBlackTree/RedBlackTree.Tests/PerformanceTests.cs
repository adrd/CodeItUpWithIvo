namespace RedBlackTree.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PerformanceTests
    {
        [Test]
        [Timeout(500)]
        public void Insert_MultipleElements_ShouldBeFast()
        {
            var rbt = new RedBlackTree<int>();

            for (var i = 0; i < 100000; i++)
            {
                rbt.Insert(i);
            }
        }

        [Test]
        [Timeout(600)]
        public void Insert_MultipleElements_ShouldHaveQuickFind()
        {
            var rbt = new RedBlackTree<int>();

            for (var i = 0; i < 100000; i++)
            {
                rbt.Insert(i);
            }
        
            Assert.AreEqual(true, rbt.Contains(99999));
        }
    }
}

