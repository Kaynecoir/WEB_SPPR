using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.Services.GameService
{
	public interface IGameService
	{
		public Task<ResponseData<GameListModel<BoardGame>>> GetGameListAsync(string? categoryNormalizedName, int page = 1);
		public Task<ResponseData<BoardGame>> GetGameByIdAsync(int id);
		public Task UpdateGameAsync(int id, BoardGame product, IFormFile? formFile);
		public Task DeleteGameAsync(int id);
		public Task<ResponseData<BoardGame>> CreateGameAsync(BoardGame product, IFormFile? formFile);
	}
}
