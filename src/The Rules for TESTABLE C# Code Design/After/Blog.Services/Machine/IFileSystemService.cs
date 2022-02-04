namespace Blog.Services.Machine
{
    using System.IO;

    public interface IFileSystemService
    {
        Stream OpenRead(string path);

        Stream OpenWrite(string path);
    }
}
