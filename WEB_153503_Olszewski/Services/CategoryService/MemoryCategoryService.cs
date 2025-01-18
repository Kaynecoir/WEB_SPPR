using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.Services.CategoryService
{
	public class MemoryCategoryService : ICategoryService
	{
		public Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{
			var categories = new List<Category>
			{
				//new Category{ID = 0, Name = "Все игры", NormalizedName = "all_game"},
                new Category{Id = 1, Name = "Карточные игры", NormalizedName = "card_game"},
				new Category{Id = 2, Name = "Головоломки", NormalizedName = "mindbreak_game"},
				new Category{Id = 3, Name = "Настольные игры", NormalizedName = "board_game"},
				new Category{Id = 3, Name = "Ролевые игры", NormalizedName = "role_game"},
			};
			var result = ResponseData<List<Category>>.Success(categories);
			return Task.FromResult(result);
		}
	}
}
