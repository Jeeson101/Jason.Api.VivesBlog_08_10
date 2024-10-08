using Microsoft.EntityFrameworkCore;
using VivesBlog.Core;
using VivesBlog.Model;

namespace VivesBlog.Services
{
    public class ArticleService
    {
        private readonly VivesBlogDbContext _dbContext;

        public ArticleService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Article> Find()
        {
            return _dbContext.Articles
                .Include(a => a.Author)
                .ToList();
        }

        //Get (by id)
        public Article? Get(int id)
        {
            return _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);
        }

        //Create
        public Article? Create(Article article)
        {
            article.PublishedDate = DateTime.UtcNow;

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();

            return article;
        }

        //Update
        public Article? Update(int id, Article article)
        {
            var dbArticle = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            if (dbArticle is null)
            {
                return null;
            }

            dbArticle.Title = article.Title;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;
            dbArticle.AuthorId = article.AuthorId;

            _dbContext.SaveChanges();

            return dbArticle;
        }

        //Delete
        public void Delete(int id)
        {
            var article = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            if (article is null)
            {
                return;
            }

            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();
        }

    }
}
