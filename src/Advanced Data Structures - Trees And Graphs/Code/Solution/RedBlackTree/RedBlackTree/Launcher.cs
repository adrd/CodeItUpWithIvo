namespace RedBlackTree
{
    public class Launcher
    {
        public static void Main()
        {
            var tree = new RedBlackTree<int>();

            tree.Insert(30);
            tree.Insert(20);
            tree.Insert(50);
            tree.Insert(15);
            tree.Insert(65);
            tree.Insert(25);
        }
    }
}