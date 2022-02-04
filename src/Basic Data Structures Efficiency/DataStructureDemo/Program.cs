namespace BasicDataStructureDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            // Array - for micro performance optimizations.
            int[] numbersArray = new int[1000];

            // List - for accessing by index.
            // FAST - Add, Count, []
            // SLOW - Insert, Remove, RemoveAt, Find, Contains... etc., LINQ
            List<int> numbersList = new List<int>();

            // Linked List - for sequential items.
            // FAST - Everything included
            // SLOW - Index, Find, LINQ
            LinkedList<int> linkedList = new LinkedList<int>();

            // Set - for unique elements and fast contains.
            // FAST - Add, Count, Contains, Remove
            // SLOW - Find all odd numbers, LINQ
            HashSet<int> numbersSet = new HashSet<int>();

            // Sorted Set - for sorted unique elements and fast contains.
            // FAST - Add, Count, Contains, Remove, GetMax() or GetMin()
            // SLOW - Find all odd numbers, LINQ
            SortedSet<int> numbersSortedSet = new SortedSet<int>();

            // Dictionary - for fast key-value access
            // FAST - Get by key, Add, Count, [], RemoveByKey, ContainsKey
            // SLOW - Find by value, Remove by value, and everything related to value, LINQ
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            // Sorted Dictionary - for fast key-value access with sorted keys
            // FAST - Get by key, Add, Count, [], RemoveByKey, GetMax() or GetMin(), ContainsKey
            // SLOW - Find by value, Remove by value, and everything related to value, LINQ
            SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();

            // Stack - for first-in-last-out situations
            // FAST - Everything included
            // SLOW - Foreach, Find, LINQ
            Stack<int> stack = new Stack<int>();

            // Queue - for first-in-first out situations
            // FAST - Everything included
            // SLOW - Foreach, Find, LINQ
            Queue<int> queue = new Queue<int>();

            // Foreach
            foreach (var number in numbersSet)
            {
                
            }

            // LINQ
            var result = numbersSet
                .Where(n => n % 2 == 0)
                .ToList();
        }
    }
}
