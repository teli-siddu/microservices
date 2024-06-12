using MediatR;
using Test.API.DataAccess;
using Test.API.Models;
using Test.API.Queries;

namespace Test.API.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonModel>
    {
        private readonly IDataAccess _dataAccess;

        public GetPersonByIdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {

            return await Task.FromResult(_dataAccess.GetPersonById(request.id));
        }
    }
}
