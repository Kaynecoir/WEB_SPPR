using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;

namespace WEB_153503_Olszewski.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WEB_153503_Olszewski.API.Data.AppDbContext _context;

        public DetailsModel(WEB_153503_Olszewski.API.Data.AppDbContext context)
        {
            _context = context;
        }

      public BoardGame BoardGame { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BoardGames == null)
            {
                return NotFound();
            }

            var boardgame = await _context.BoardGames.FirstOrDefaultAsync(m => m.Id == id);
            if (boardgame == null)
            {
                return NotFound();
            }
            else 
            {
                BoardGame = boardgame;
            }
            return Page();
        }
    }
}
