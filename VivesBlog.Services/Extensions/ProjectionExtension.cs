using VivesBlog.Dto.Result;
using VivesBlog.Model;

namespace VivesBlog.Services.Extensions
{
	public static class ProjectionExtension
	{
		public static IQueryable<ArticleResult> Project(this IQueryable<Article> query)
		{
			return query.Select(o => new ArticleResult
			{
				Id = o.Id,
				Title = o.Title,
				AuthorId = o.AuthorId,
				AuthorName = o.Author.FirstName,
				Description = o.Description,
				Content = o.Content
			});
		}

		public static IQueryable<PersonResult> Project(this IQueryable<Person> query)
		{
			return query.Select(p => new PersonResult
			{
				Id = p.Id,
				FirstName = p.FirstName,
				LastName = p.LastName,
				Email = p.Email,
				ArticleCount = p.Articles.Count
			});

		}

	}
}
