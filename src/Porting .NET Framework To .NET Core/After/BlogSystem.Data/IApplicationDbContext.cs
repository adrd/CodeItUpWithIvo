namespace BlogSystem.Data
{
    using System;

    using BlogSystem.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IApplicationDbContext : IDisposable
    {
        DbSet<BlogPost> BlogPosts { get; set; }

        DbSet<Page> Pages { get; set; }
        
        DbSet<Setting> Settings { get; set; }

        DbSet<PostComment> PostComments { get; set; }

        DbSet<ApplicationUser> Users { get; set; }

        DbSet<Video> Videos { get; set; }

        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<T> Set<T>() where T : class;
    }
}
