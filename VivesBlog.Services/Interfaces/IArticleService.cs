using Vives.Services.Model;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;

namespace VivesBlog.Services.Interfaces
{
	public interface IArticleService
	{
		Task<IList<ArticleResult>> Find();
		Task<ServiceResult<ArticleResult>> Get(int id);
		Task<ServiceResult<ArticleResult>> Create(ArticleRequest request);
		Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request);
		Task<ServiceResult> Delete(int id);
	}
}
