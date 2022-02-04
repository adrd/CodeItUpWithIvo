namespace Blog.Services.Machine
{
    using System.IO;

    public class FileSystemService : IFileSystemService
    {
        public Stream OpenRead(string path) => File.OpenRead(path);

        public Stream OpenWrite(string path) => File.OpenWrite(path);
    }
}
