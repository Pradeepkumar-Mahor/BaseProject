using DataAccess.Core.Interface;
using Domain.Core.Tables;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var list = _categoryRepository.GetAll();

            return View(list);
        }
    }
}