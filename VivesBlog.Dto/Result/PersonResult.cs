using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VivesBlog.Dto.Result
{
	public class PersonResult
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Email { get; set; }

		public int ArticleCount { get; set; }
	}
}
