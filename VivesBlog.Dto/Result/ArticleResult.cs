using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Result
{
	public class ArticleResult
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public int? AuthorId { get; set; }
		public string? AuthorName { get; set; }
		public DateTime PublishedDate { get; set; }

		public string Description { get; set; }
		public string Content { get; set; }
	}
}
