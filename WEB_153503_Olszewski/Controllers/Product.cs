using Microsoft.AspNetCore.Mvc;
using WEB_153503_Olszewski.Services.CategoryService;
using WEB_153503_Olszewski.Services.GameService;

namespace WEB_153503_Olszewski.Controllers
{
	public class Product : Controller
	{
		IGameService _service;
		ICategoryService _categoryService;
		string? category;

		public Product(ICategoryService categoryService, IGameService gameService)
		{
			_service = gameService;
			_categoryService = categoryService;
			category = "role_game";
		}
		public async Task<IActionResult> Index(string? category)
		{
			var productResponse = await _service.GetGameListAsync(category);
			if (category != null)
				ViewData["currentCategory"] = _categoryService.GetCategoryListAsync().Result.Data.Find(c => c.NormalizedName == category).Name;
			else
				ViewData["currentCategory"] = "Все";
			if (!productResponse.Successfull) 
				return NotFound(productResponse.ErrorMessage);
			ViewBag.CategoryList = _categoryService.GetCategoryListAsync().Result.Data;

            return View(productResponse.Data);
		}
	}
}
