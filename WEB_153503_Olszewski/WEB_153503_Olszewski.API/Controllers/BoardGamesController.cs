﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;

namespace WEB_153503_Olszewski.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoardGamesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BoardGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardGame>>> GetBoardGames()
        {
          if (_context.BoardGames == null)
          {
              return NotFound();
          }
            return await _context.BoardGames.ToListAsync();
        }

        // GET: api/BoardGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardGame>> GetBoardGame(int id)
        {
          if (_context.BoardGames == null)
          {
              return NotFound();
          }
            var boardGame = await _context.BoardGames.FindAsync(id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return boardGame;
        }

        // PUT: api/BoardGames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardGame(int id, BoardGame boardGame)
        {
            if (id != boardGame.Id)
            {
                return BadRequest();
            }

            _context.Entry(boardGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardGameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BoardGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardGame>> PostBoardGame(BoardGame boardGame)
        {
          if (_context.BoardGames == null)
          {
              return Problem("Entity set 'AppDbContext.BoardGames'  is null.");
          }
            _context.BoardGames.Add(boardGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoardGame", new { id = boardGame.Id }, boardGame);
        }

        // DELETE: api/BoardGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardGame(int id)
        {
            if (_context.BoardGames == null)
            {
                return NotFound();
            }
            var boardGame = await _context.BoardGames.FindAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }

            _context.BoardGames.Remove(boardGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardGameExists(int id)
        {
            return (_context.BoardGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}