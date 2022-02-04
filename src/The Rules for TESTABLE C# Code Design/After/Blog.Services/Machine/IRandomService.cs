namespace Blog.Services.Machine
{
    public interface IRandomService
    {
        int Next(int min, int max);
    }
}
