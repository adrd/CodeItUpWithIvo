namespace ExtensionMethods.InterfaceAbstraction.Before
{
    using System.IO;

    public class CatService : IExportable
    {
        public FileInfo SaveToFile(object model, string filePath)
        {
            // Logic
            return null;
        }
    }
}
