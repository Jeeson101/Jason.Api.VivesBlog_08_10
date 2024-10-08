using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Core;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;
using VivesBlog.Model;
using VivesBlog.Services.Extensions;
using VivesBlog.Services.Interfaces;

namespace VivesBlog.Services
{
    public class PersonService : IPersonService
    {
        private readonly VivesBlogDbContext _dbContext;

        public PersonService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<PersonResult>> Find()
        {
            return await _dbContext.People
	            .Project()
				.ToListAsync();
        }

        //Get (by id)
        public async Task<ServiceResult<PersonResult>> Get(int id)
        {
            var serviceResult = new ServiceResult<PersonResult>();

			var person = await _dbContext.People
		                 .Project()
		                 .FirstOrDefaultAsync(p => p.Id == id);

			serviceResult.Data = person;

			return serviceResult;
		}

        //Create
        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var serviceResult = new ServiceResult<PersonResult>();

			var person = new Person
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email
			};

			_dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        //Update
        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var serviceResult = new ServiceResult<PersonResult>();

			var person = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await _dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
	        var serviceResult = new ServiceResult();

	        var person = _dbContext.People
		        .FirstOrDefault(p => p.Id == id);

	        if (person is null)
	        {
		        serviceResult.NotFound(nameof(Person), id);
		        return serviceResult;
	        }

	        _dbContext.People.Remove(person);
	        await _dbContext.SaveChangesAsync();

	        serviceResult.Deleted(nameof(Person));
	        return serviceResult;
		}

    }
}
