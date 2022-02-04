namespace ExtensionMethods.InterfaceAbstraction.After
{
    using System.IO;

    public interface IExportable
    {
        void Save(object model, Stream output);
    }
}
