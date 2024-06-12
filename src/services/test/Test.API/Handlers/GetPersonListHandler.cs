using MediatR;
using Test.API.DataAccess;
using Test.API.Models;
using Test.API.Queries;

namespace Test.API.Handlers
{
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, List<PersonModel>>
    {
        private readonly IDataAccess _dataAccess;

        public GetPersonListHandler(IDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }
        public async Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dataAccess.GetPeople());
        }
    }

}
