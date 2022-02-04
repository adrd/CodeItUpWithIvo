namespace DependencyInjection
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; } = "My Cool Cat";
    }
}
