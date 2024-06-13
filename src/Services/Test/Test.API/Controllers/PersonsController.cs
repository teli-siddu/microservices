using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.API.Commands;
using Test.API.Models;
using Test.API.Queries;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<PersonModel>>> Get()
        {
            var people = await _mediator.Send(new GetPersonListQuery());
            return Ok(people);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<PersonModel>>> Get(int id)
        {
            var people = await _mediator.Send(new GetPersonByIdQuery(id));
            return Ok(people);
        }
        [HttpPost]
        public async Task<ActionResult<List<PersonModel>>> Post(PersonModel personModel)
        {
            var people = await _mediator.Send(new InsertPersonCommand(personModel.FirstName,personModel.LastName));
            return Ok(people);
        }
      
    }
}

