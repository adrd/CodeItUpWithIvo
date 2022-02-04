namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System;

    public interface IBlogUrlGenerator
    {
        string GenerateUrl(int id, string title, DateTime createdOn);
    }
}
