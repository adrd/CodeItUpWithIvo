namespace MyTestRunner.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class SubjectAttribute : Attribute
    {
        public SubjectAttribute(string name) => this.Name = name;

        public string Name { get; }
    }
}
