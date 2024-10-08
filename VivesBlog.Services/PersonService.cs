using VivesBlog.Core;
using VivesBlog.Model;

namespace VivesBlog.Services
{
    public class PersonService
    {
        private readonly VivesBlogDbContext _dbContext;

        public PersonService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Person> Find()
        {
            return _dbContext.People
                .ToList();
        }

        //Get (by id)
        public Person? Get(int id)
        {
            return _dbContext.People
                .FirstOrDefault(p => p.Id == id);
        }

        //Create
        public Person? Create(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();

            return person;
        }

        //Update
        public Person? Update(int id, Person person)
        {
            var dbPerson = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            _dbContext.SaveChanges();

            return dbPerson;
        }

        //Delete
        public void Delete(int id)
        {
            var person = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (person is null)
            {
                return;
            }

            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }

    }
}
