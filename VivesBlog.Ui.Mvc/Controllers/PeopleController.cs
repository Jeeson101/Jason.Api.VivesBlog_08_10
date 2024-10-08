using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.Mvc.Controllers
{
    //[Authorize]
    public class PeopleController : Controller
    {
        private readonly PersonSdk _personService;

        public PeopleController(PersonSdk personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personService.Find();

            return View(people);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
				var person = new PersonResult
				{
					FirstName = request.FirstName,
					LastName = request.LastName,
					Email = request.Email
				};

                return View(person);
			}

            await _personService.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var response = await _personService.Get(id);

            if (!response.IsSuccess || response.Data is null)
            {
	            return RedirectToAction("Index");
            }

			return View(response.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]PersonRequest request)
        {
	        if (!ModelState.IsValid)
	        {
		        var result = await _personService.Get(id);
		        if (!result.IsSuccess || result.Data is null)
		        {
			        return RedirectToAction("Index");
		        }

		        var person = result.Data;
		        person.FirstName = request.FirstName;
		        person.LastName = request.LastName;
		        person.Email = request.Email;

		        return View(person);
	        }

	        await _personService.Update(id, request);

	        return RedirectToAction("Index");
		}


        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
