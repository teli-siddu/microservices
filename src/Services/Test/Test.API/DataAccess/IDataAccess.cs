using Test.API.Models;

namespace Test.API.DataAccess
{
    public interface IDataAccess
    {
        List<PersonModel> GetPeople();
        PersonModel InsertPerson(string firstName, string lastName);
        PersonModel GetPersonById(int id);
    }
}