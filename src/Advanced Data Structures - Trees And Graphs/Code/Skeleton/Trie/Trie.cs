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
            => throw new NotImplementedException();

        public bool Contains(string key)
            => throw new NotImplementedException();

        public void Insert(string key, Value val)
            => throw new NotImplementedException();

        public IEnumerable<string> GetByPrefix(string prefix)
            => throw new NotImplementedException();
    }
}