using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Core;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;
using VivesBlog.Model;
using VivesBlog.Services.Extensions;

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
        public async Task<IList<ArticleResult>> Find()
        {
            return await _dbContext.Articles
                .Project()
                .ToListAsync();
        }

        //Get (by id)
        public async Task<ServiceResult<ArticleResult>> Get(int id)
        {
            var serviceResult = new ServiceResult<ArticleResult>();

			var article = await _dbContext.Articles
				.Project()
				.FirstOrDefaultAsync(a => a.Id == id);

            serviceResult.Data = article;

			return serviceResult;

		}

        //Create
        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
	        var serviceResult = new ServiceResult<ArticleResult>();

            var article = new Article
			{
				Title = request.Title,
				Description = request.Description,
				Content = request.Content,
				AuthorId = request.AuthorId
			};

			article.PublishedDate = DateTime.UtcNow;

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        //Update
        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
	        var serviceResult = new ServiceResult<ArticleResult>();


			var article = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            article.Title = request.Title;
            article.Description = request.Description;
            article.Content = request.Content;
            article.AuthorId = request.AuthorId;

            await _dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var serviceResult = new ServiceResult();

			var article = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

			if (article is null)
			{
				serviceResult.NotFound(nameof(Article), id);
				return serviceResult;
			}

			_dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync();

            serviceResult.Deleted(nameof(Article));
            return serviceResult;
        }

    }
}
