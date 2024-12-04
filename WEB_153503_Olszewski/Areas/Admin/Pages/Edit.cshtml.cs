using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;

namespace WEB_153503_Olszewski.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WEB_153503_Olszewski.API.Data.AppDbContext _context;

        public EditModel(WEB_153503_Olszewski.API.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BoardGame BoardGame { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BoardGames == null)
            {
                return NotFound();
            }

            var boardgame =  await _context.BoardGames.FirstOrDefaultAsync(m => m.Id == id);
            if (boardgame == null)
            {
                return NotFound();
            }
            BoardGame = boardgame;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BoardGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardGameExists(BoardGame.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BoardGameExists(int id)
        {
          return (_context.BoardGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
