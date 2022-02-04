namespace Trie
{
    using System;
    using System.Collections.Generic;

    public class Trie<Value>
    {
        private Node root;

        private class Node
        {
            public Value Val { get; set; }

            public bool IsTerminal { get; set; }

            public Dictionary<char, Node> Next { get; } = new Dictionary<char, Node>();
        }

        public Value GetValue(string key)
        {
            var x = this.GetNode(this.root, key, 0);

            if (x == null || !x.IsTerminal)
            {
                throw new InvalidOperationException();
            }

            return x.Val;
        }

        public bool Contains(string key)
        {
            var node = this.GetNode(this.root, key, 0);
            return node != null && node.IsTerminal;
        }

        public void Insert(string key, Value val) 
            => this.root = this.Insert(this.root, key, val, 0);

        public IEnumerable<string> GetByPrefix(string prefix)
        {
            var results = new Queue<string>();

            var node = this.GetNode(this.root, prefix, 0);

            this.Collect(node, prefix, results);
        
            return results;
        }

        private Node GetNode(Node nextNode, string key, int distance)
        {
            if (nextNode == null)
            {
                return null;
            }

            if (distance == key.Length)
            {
                return nextNode;
            }

            Node node = null;
            var symbol = key[distance];

            if (nextNode.Next.ContainsKey(symbol))
            {
                node = nextNode.Next[symbol];
            }

            return this.GetNode(node, key, distance + 1);
        }

        private Node Insert(Node newNode, string key, Value val, int distance)
        {
            if (newNode == null)
            {
                newNode = new Node();
            }

            if (distance == key.Length)
            {
                newNode.Val = val;
                newNode.IsTerminal = true;

                return newNode;
            }

            Node node = null;
            var c = key[distance];

            if (newNode.Next.ContainsKey(c))
            {
                node = newNode.Next[c];
            }

            newNode.Next[c] = this.Insert(node, key, val, distance + 1);

            return newNode;
        }

        private void Collect(Node node, string prefix, Queue<string> results)
        {
            if (node == null)
            {
                return;
            }

            if (node.Val != null && node.IsTerminal)
            {
                results.Enqueue(prefix);
            }

            foreach (var c in node.Next.Keys)
            {
                this.Collect(node.Next[c], prefix + c, results);
            }
        }
    }
}