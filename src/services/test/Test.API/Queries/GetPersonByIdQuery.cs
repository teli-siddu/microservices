using MediatR;
using Test.API.Models;

namespace Test.API.Queries
{
    public record GetPersonByIdQuery(int id):IRequest<PersonModel>;
}
