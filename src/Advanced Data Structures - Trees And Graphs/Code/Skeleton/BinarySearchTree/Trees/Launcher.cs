namespace Trees
{
    using System;

    public class Launcher
    {
        public static void Main()
        {
            var BST = new BinarySearchTree<int>();

            BST.Insert(5);
            BST.Insert(3);
            BST.Insert(4);
            BST.Insert(7);
            BST.Insert(6);

            foreach (var num in BST.Range(3, 7))
            {
                Console.WriteLine(num);
            }
        }
    }
}