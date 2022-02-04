namespace ExtensionMethods.InterfaceAbstraction.Before
{
    using System.IO;

    public interface IExportable
    {
        FileInfo SaveToFile(object model, string filePath);

        // byte[] SaveToMemory(object model);
    }
}
