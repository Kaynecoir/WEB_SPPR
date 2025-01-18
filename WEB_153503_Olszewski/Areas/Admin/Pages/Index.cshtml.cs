using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Services.GameService;

namespace WEB_153503_Olszewski.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;

        public IndexModel(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IList<BoardGame> BoardGame { get;set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNo = 1)
        {
            var response = await _gameService.GetGameListAsync(null, pageNo);
            if (!response.Successfull)
            {
                return NotFound();
            }
            BoardGame = response.Data.Items;
            CurrentPage = response.Data.CurrentPage;
            TotalPages = response.Data.TotalPages;
            return Page();
        }
    }
}
