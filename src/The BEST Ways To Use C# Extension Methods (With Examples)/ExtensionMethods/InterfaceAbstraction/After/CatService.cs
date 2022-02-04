namespace ExtensionMethods.InterfaceAbstraction.After
{
    using System.IO;

    public class CatService : IExportable
    {
        public void Save(object model, Stream output)
        {
            // Logic
        }
    }
}
