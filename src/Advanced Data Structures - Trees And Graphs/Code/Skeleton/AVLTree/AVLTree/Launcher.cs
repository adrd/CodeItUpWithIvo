namespace AVLTree
{
    public class Launcher
    {
        public static void Main()
        {
            var tree = new AVL<int>();

            tree.Insert(4);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
        }
    }
}
