using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;

namespace WEB_153503_Olszewski.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WEB_153503_Olszewski.API.Data.AppDbContext _context;

        public CreateModel(WEB_153503_Olszewski.API.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public BoardGame BoardGame { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BoardGames == null || BoardGame == null)
            {
                return Page();
            }

            _context.BoardGames.Add(BoardGame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
