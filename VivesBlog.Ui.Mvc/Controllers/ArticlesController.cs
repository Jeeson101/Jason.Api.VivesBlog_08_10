using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Ui.Mvc.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly ArticleService _articleService;
        private readonly PersonService _personService;

        public ArticlesController(
            ArticleService articleService,
            PersonService personService)
        {
            _articleService = articleService;
            _personService = personService;
        }
        public IActionResult Index()
        {
            var articles = _articleService.Find();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView(article);
            }

            _articleService.Create(article);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var article = _articleService.Get(id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView(article);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] Article article)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView(article);
            }

            _articleService.Update(id, article);

            return RedirectToAction("Index");
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _articleService.Delete(id);

            return RedirectToAction("Index");
        }


        private IActionResult CreateEditView(Article? article = null)
        {
            var authors = _personService.Find();
            ViewBag.Authors = authors;

            return View(article);
        }
    }
}
