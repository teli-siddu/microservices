using MediatR;
using Test.API.Models;

namespace Test.API.Queries
{
    public record GetPersonListQuery():IRequest<List<PersonModel>>;
   
}
