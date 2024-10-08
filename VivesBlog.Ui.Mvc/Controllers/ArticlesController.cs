using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.Mvc.Controllers
{ 
	public class ArticlesController : Controller
    {
        private readonly ArticleSdk _articleService;
        private readonly PersonSdk _personService;

        public ArticlesController(
            ArticleSdk articleService,
            PersonSdk personService)
        {
            _articleService = articleService;
            _personService = personService;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.Find();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
	        return await CreateEditView();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
	            var article = new ArticleResult
	            {
					Title = request.Title,
					Description = request.Description,
					Content = request.Content,
					AuthorId = request.AuthorId
				};

	            return await CreateEditView(article);
			}

            await _articleService.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
	        var result = await _articleService.Get(id);

	        if (result?.Data is null)
	        {
		        return RedirectToAction("Index");
	        }

	        return await CreateEditView(result.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ArticleRequest request)
        {

			if (!ModelState.IsValid)
	        {
		        var result = await _articleService.Get(id);

		        if (!result.IsSuccess || result.Data is null)
		        {
					return RedirectToAction("Index");
				}

		        var article = result.Data;
		        article.Title = request.Title;
		        article.Description = request.Description;

				return await CreateEditView(article);

			}

			await _articleService.Update(id, request);

	        return RedirectToAction("Index");
        }


		[HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleService.Delete(id);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> CreateEditView(ArticleResult? result = null)
        {
	        var authors = await _personService.Find();
	        ViewBag.Authors = authors;


	        return View(result);
        }

	}
}
