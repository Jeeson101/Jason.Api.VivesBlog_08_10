using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Request;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly PersonService _personService;

		public PersonController(PersonService personService)
		{
			_personService = personService;
		}


		[HttpGet]
		public async Task<IActionResult> Find()
		{
			var organizations = await _personService.Find();
			return Ok(organizations);
		}

		//Get (one) GET
		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get([FromRoute] int id)
		{
			var result = await _personService.Get(id);
			return Ok(result);
		}

		//Create POST
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PersonRequest request)
		{
			var result = await _personService.Create(request);
			return Ok(result);
		}

		//Update PUT
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonRequest request)
		{
			var result = await _personService.Update(id, request);
			return Ok(result);
		}

		//Delete DELETE
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var result = await _personService.Delete(id);
			return Ok(result);
		}
	}
}
