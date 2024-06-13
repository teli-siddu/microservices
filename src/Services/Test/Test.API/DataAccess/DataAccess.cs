using Test.API.Models;

namespace Test.API.DataAccess
{
    public class DataAccess : IDataAccess
    {
        List<PersonModel> people = new();
        public DataAccess()
        {
            people.Add(new PersonModel { Id = 1, FirstName = "siddu", LastName = "teli" });
            people.Add(new PersonModel { Id = 2, FirstName = "Rakesh", LastName = "K" });
        }
        public List<PersonModel> GetPeople()
        {
            return people;
        }
        public PersonModel GetPersonById(int id)
        {
            return people.FirstOrDefault(x=>x.Id==id);
        }
        public PersonModel InsertPerson(string firstName, string lastName)
        {
            PersonModel person = new() { FirstName = firstName, LastName = lastName };
            person.Id = people.Max(x => x.Id) + 1;
            people.Add(person);
            return person;
        }
    }
}
