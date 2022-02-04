namespace ExtensionMethods.InterfaceAbstraction.Before
{
    using System.IO;

    public class DogService : IExportable
    {
        public FileInfo SaveToFile(object model, string filePath)
        {
            // Logic
            return null;
        }
    }
}
