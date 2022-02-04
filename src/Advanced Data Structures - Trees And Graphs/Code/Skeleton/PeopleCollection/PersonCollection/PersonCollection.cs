namespace PersonCollection
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class PersonCollection : IPersonCollection
    {
        public int Count
            => throw new NotImplementedException();

        public bool AddPerson(string email, string name, int age, string town)
            => throw new NotImplementedException();

        public Person FindPerson(string email)
            => throw new NotImplementedException();

        public bool DeletePerson(string email)
            => throw new NotImplementedException();

        public IEnumerable<Person> FindPersons(string emailDomain)
            => throw new NotImplementedException();

        public IEnumerable<Person> FindPersons(string name, string town)
            => throw new NotImplementedException();

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
            => throw new NotImplementedException();

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
            => throw new NotImplementedException();
    }
}
