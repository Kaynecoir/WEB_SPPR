using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;
using WEB_153503_Olszewski.Services.CategoryService;

namespace WEB_153503_Olszewski.Services.GameService
{
	public class MemoryGameService : IGameService
	{
		List<BoardGame> _boardGames;
		List<Category> _categories; 
		IConfiguration _config;
        int _pageSize;

		public MemoryGameService([FromServices] IConfiguration config, ICategoryService categoryService, int pageNo = 1)
		{
            _config = config;
			_categories = categoryService.GetCategoryListAsync().Result.Data;
			_pageSize = config.GetValue<int>("ItemsPerPage");

			SetupData();
		}

        public Task<ResponseData<BoardGame>> CreateGameAsync(BoardGame product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		public Task DeleteGameAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<BoardGame>> GetGameByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<GameListModel<BoardGame>>> GetGameListAsync(
            string? categoryNormalizedName,
            int pageNo)
        {
                var itemsPerPage = Convert.ToInt32(_config["ItemsPerPage"]);
                var result = new ResponseData<GameListModel<BoardGame>>();
                var data = _boardGames.Where(game => categoryNormalizedName == null ||
                    game.Category.NormalizedName.Equals(categoryNormalizedName))
                    .ToList();
                var pagesTotal = data.Count / itemsPerPage + ((data.Count % itemsPerPage) != 0 ? 1 : 0);
                data = data.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList();
                result.Data = new GameListModel<BoardGame>
                {
                    Items = data,
                    CurrentPage = pageNo,
                    TotalPages = pagesTotal
                };
                return Task.FromResult(result);
            }

		public Task UpdateGameAsync(int id, BoardGame product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		private void SetupData()
		{
			_boardGames = new List<BoardGame>
			{
				new BoardGame 
				{
					Id = 1, 
					Name="Dungeon and Dragons",
					Description="Игра в стиле фэнтези.", 
					Price = 191.0f, 
					Image="Images/dnd.jpg",
					Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("role_game"))
				},
				new BoardGame 
				{
					Id = 2, 
					Name="Vampire: the Masquarade",
					Description="Игра в стиле темного фэнтези.", 
					Price = 181.0f, 
					Image="Images/vtm.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("role_game"))
				},
				new BoardGame 
				{
					Id = 3, 
					Name="Pathfinder",
					Description="Игра в стиле фэнтези.", 
					Price = 171.0f, 
					Image="Images/path.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("role_game"))
				},
				new BoardGame 
				{
					Id = 4, 
					Name="Catan",
					Description="Колонизаторы.", 
					Price = 141.0f, 
					Image="Images/catan.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("board_game"))
				},
                new BoardGame 
				{
					Id = 5, 
					Name="Citadel",
                    Description="Цитадель.", 
					Price = 71.0f, 
					Image="Images/citadel.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("board_game"))
				},
                new BoardGame 
				{
					Id = 6, 
					Name="Bang!",
                    Description="Бэнг!", 
					Price = 51.0f, 
					Image="Images/bang.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("board_game"))
				},
                new BoardGame 
				{
					Id = 7, 
					Name="Dungeon and Dragons V2",
                    Description="Игра в стиле фэнтези.", 
					Price = 191.0f, 
					Image="Images/dnd.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("role_game"))
				},
                new BoardGame
                {
                    Id = 8,
                    Name="Bang! Star Wars",
                    Description="Бэнг по Звездным Войнам!",
                    Price = 71.0f, 
					Image="Images/bang_sw.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 9,
                    Name="Carcassonne",
                    Description="Каркассоны! Средневековье и тайлы!",
                    Price = 71.0f, 
					Image="Images/carcassonne.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 10,
                    Name="UNO",
                    Description="Крикни UNO раньше других!",
                    Price = 71.0f, 
					Image="Images/uno.jpg",
                    Mime="JPEG",
                    Category=_categories.Find(c=>c.NormalizedName.Equals("card_game"))
                },
            };
		}
	}
}
