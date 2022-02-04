namespace Trees
{
    using System;

    public class Launcher
    {
        public static void Main()
        {
            var binaryTree =
                new BinaryTree<string>("*",
                    new BinaryTree<string>("+",
                        new BinaryTree<string>("3"),
                        new BinaryTree<string>("2")),
                    new BinaryTree<string>("-",
                        new BinaryTree<string>("9"),
                        new BinaryTree<string>("6")));
        
            Console.WriteLine("Binary tree (indented, pre-order):");
            binaryTree.PrintIndentedPreOrder();
        
            Console.Write("Binary tree nodes (in-order):");
            binaryTree.EachInOrder(c => Console.Write(" " + c));
            Console.WriteLine();
        
            Console.Write("Binary tree nodes (post-order):");
            binaryTree.EachPostOrder(c => Console.Write(" " + c));
            Console.WriteLine();
        }
    }
}
