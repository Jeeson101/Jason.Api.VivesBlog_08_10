using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dto.Request
{
	public class ArticleRequest
	{
		[Required]
		public string Title { get; set; }
		public int AuthorId { get; set; }

		[Required]
		public required string Description { get; set; }
		[Required]
		public required string Content { get; set; }

	}
}
