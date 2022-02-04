namespace ExtensionMethods.InterfaceAbstraction.After
{
    using System.IO;

    public class DogService : IExportable
    {
        public void Save(object model, Stream output)
        {
            // Logic
        }
    }
}
