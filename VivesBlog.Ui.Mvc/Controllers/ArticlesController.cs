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
	        return await CreateEditView((ArticleRequest?)null);
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
	        var articleResult = await _articleService.Get(id);

	        if (articleResult?.Data is null)
	        {
		        return RedirectToAction("Index");
	        }

	        return await CreateEditView(articleResult.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ArticleRequest article)
        {
	        if (!ModelState.IsValid)
	        {
		        return await CreateEditView(article);
	        }

	        await _articleService.Update(id, article);

	        return RedirectToAction("Index");
        }


		[HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _articleService.Delete(id);

            return RedirectToAction("Index");
        }


        private async Task<IActionResult> CreateEditView(ArticleRequest? request = null)
        {
	        var authors = await _personService.Find();
	        ViewBag.Authors = authors;

	        return View(request);
        }

        private async Task<IActionResult> CreateEditView(ArticleResult? result = null)
        {
	        var authors = await _personService.Find();
	        ViewBag.Authors = authors;


	        return View(result);
        }

	}
}
