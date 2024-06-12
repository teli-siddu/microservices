using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Test.API.Commands;
using Test.API.DataAccess;
using Test.API.Models;

namespace Test.API.Handlers
{
    public class InsertPersonHandler : IRequestHandler<InsertPersonCommand, PersonModel>
    {
        private readonly IDataAccess _dataAccess;

        public InsertPersonHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<PersonModel> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
          return await Task.FromResult(_dataAccess.InsertPerson(request.FirstName, request.LastName));
        }
    }
}
