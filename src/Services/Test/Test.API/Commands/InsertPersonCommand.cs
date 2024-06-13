using MediatR;
using Test.API.Models;

namespace Test.API.Commands
{
    public record InsertPersonCommand(string FirstName, string LastName):IRequest<PersonModel>;
   
}
