using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Result
{
	public class ArticleResult
	{
		public int Id { get; set; }
		public required string Title { get; set; }

		public int? AuthorId { get; set; }
		public string? AuthorName { get; set; }
		public DateTime PublishedDate { get; set; }

		public required string Description { get; set; }
		public required string Content { get; set; }
	}
}
