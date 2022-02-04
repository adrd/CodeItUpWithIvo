namespace ExtensionMethods.InterfaceAbstraction.After
{
    using System.IO;

    public static class ExportServiceExtensions
    {
        public static FileInfo SaveToFile(this IExportable exportable, object model, string filePath)
        {
            using (var output = File.Create(filePath))
            {
                exportable.Save(model, output);
                return new FileInfo(filePath);
            }
        }

        public static byte[] SaveToMemory(this IExportable exportable, object model)
        {
            using (var output = new MemoryStream())
            {
                exportable.Save(model, output);
                return output.ToArray();
            }
        }
    }
}
