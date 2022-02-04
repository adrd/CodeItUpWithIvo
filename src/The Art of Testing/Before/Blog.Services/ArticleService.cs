﻿namespace Blog.Services
{
    using System;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext db;

        public ArticleService() 
            => this.db = new BlogDbContext();

        public async Task<IEnumerable<ArticleListingServiceModel>> All(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            => await this.All<ArticleListingServiceModel>(page, pageSize, publicOnly);

        public async Task<IEnumerable<TModel>> All<TModel>(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            where TModel : class
        {
            var query = this.db.Articles.AsQueryable();

            if (publicOnly)
            {
                query = query.Where(a => a.IsPublic);
            }

            return await query
                .OrderByDescending(a => a.PublishedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TModel>(Mapper.Configuration)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> AllIds()
            => await this.db
                .Articles
                .Where(a => a.IsPublic)
                .Select(a => a.Id)
                .ToListAsync();

        public async Task<IEnumerable<ArticleForUserListingServiceModel>> ByUser(string userId)
            => await this.db
                .Articles
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.PublishedOn)
                .ProjectTo<ArticleForUserListingServiceModel>(Mapper.Configuration)
                .ToListAsync();

        public async Task<bool> IsByUser(int id, string userId)
            => await this.db
                .Articles
                .AnyAsync(a => a.Id == id && a.UserId == userId);

        public async Task<ArticleDetailsServiceModel> Details(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>(Mapper.Configuration)
                .FirstOrDefaultAsync();

        public async Task<int> Total()
            => await this.db
                .Articles
                .Where(a => a.IsPublic)
                .CountAsync();

        public async Task<int> Add(string title, string content, string userId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                UserId = userId
            };

            this.db.Articles.Add(article);

            await this.db.SaveChangesAsync();

            return article.Id;
        }

        public async Task Edit(int id, string title, string content)
        {
            var article = await this.db.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            article.Title = title;
            article.Content = content;
            article.IsPublic = false;

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var article = await this.db.Articles.FindAsync(id);
            this.db.Articles.Remove(article);

            await this.db.SaveChangesAsync();
        }

        public async Task ChangeVisibility(int id)
        {
            var article = await this.db.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            article.IsPublic = !article.IsPublic;

            if (article.PublishedOn == null)
            {
                article.PublishedOn = DateTime.UtcNow;    
            }

            await this.db.SaveChangesAsync();
        }
    }
}
