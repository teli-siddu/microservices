using EmployeesSearch.API.Models;
using EmployeesSearch.API.RequestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace EmployeesSearch.API.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class EmployeesSearchController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery]SearchParams searchParams)
        {
            var query= DB.PagedSearch<Item,Item>();
            query.Sort(x => x.Ascending(a => a.FirstName));

            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }
            query = searchParams.OrderBy switch
            {
                "departmment" => query.Sort(x => x.Ascending(x => x.Department)),
                "position"=> query.Sort(x => x.Ascending(x => x.Position)),
                _=>query.Sort(x=>x.Ascending(x=>x.ID))
            };
            if (!string.IsNullOrEmpty(searchParams.Position))
            {
                query.Match(Search.Full, searchParams.Position);
            }
            //query = searchParams.FilterBy switch
            //{
            //    "departmment" => query.Match(x => x.Department==),
            //    "position" => query.Sort(x => x.Ascending(x => x.Position)),
            //    _ => query.Sort(x => x.Ascending(x => x.ID))
            //};

            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);
            var result = await query.ExecuteAsync();
            return Ok(new
            {
                Results = result.Results,
                PageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}
