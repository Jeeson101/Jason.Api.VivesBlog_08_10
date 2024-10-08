using Vives.Services.Model;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;

namespace VivesBlog.Services.Interfaces
{
	public interface IPersonService
	{
		Task<IList<PersonResult>> Find();
		Task<ServiceResult<PersonResult>> Get(int id);
		Task<ServiceResult<PersonResult>> Create(PersonRequest request);
		Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request);
		Task<ServiceResult> Delete(int id);
	}
}
