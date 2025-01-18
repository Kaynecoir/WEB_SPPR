using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.API.Service.GameService
{
    public class BoardGameService : IGameService
    {
        private AppDbContext _dbContext;
        private IWebHostEnvironment _env;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly int _maxPageSize = 20;

        public BoardGameService(AppDbContext dbContext,
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseData<GameListModel<BoardGame>>> GetBoardGameListAsync(
            string? categoryNormalizedName,
            int pageNo = 1, 
            int pageSize = 3
            )
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }

            var query = _dbContext.BoardGames.AsQueryable();
            var dataList = new GameListModel<BoardGame>();
            
            query = query.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));
            


            // количество элементов в списке
            var count = await query.CountAsync();
            if (count == 0)
            {
                return ResponseData<GameListModel<BoardGame>>.Success(dataList);
            }

            // количество страниц
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (pageNo > totalPages)
            {
                return ResponseData<GameListModel<BoardGame>>.Error("No such page");
            }
            dataList.Items = await query
                .OrderBy(d => d.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;
            
            return ResponseData<GameListModel<BoardGame>>.Success(dataList);
        }

        public async Task<ResponseData<BoardGame>> CreateBoardGameAsync(BoardGame boardGame, IFormFile? formFile)
        {
            try
            {
                var b = _dbContext.BoardGames.Add(boardGame).Entity;
                await _dbContext.SaveChangesAsync();
                if (formFile != null)
                {
                    b.Image = (await SaveImageAsync(b.Id, formFile)).Data;
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<BoardGame>
                {
                    Successfull = false,
                    ErrorMessage = ex.Message
                };
            }

            return new ResponseData<BoardGame>
            {
                Data = boardGame,
                Successfull = true,
            };
        }

        public async Task DeleteBoardGameAsync(int id)
        {
            BoardGame? boardGame = await _dbContext.BoardGames.FindAsync(id);
            if (boardGame != null)
            {
                if (!String.IsNullOrEmpty(boardGame.Image))
                {
                    var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                    var prevImage = Path.Combine(imagesFolder, Path.GetFileName(boardGame.Image));
                    if (File.Exists(prevImage))
                    {
                        File.Delete(prevImage);
                    }
                }
                _dbContext.BoardGames.Remove(boardGame);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ResponseData<BoardGame>> GetBoardGameByIdAsync(int id)
        {
            BoardGame? book = await _dbContext.BoardGames.Include("Category").FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return new ResponseData<BoardGame>
                {
                    Successfull = false,
                    ErrorMessage = "No such book"
                };
            }
            else
            {
                return new ResponseData<BoardGame>
                {
                    Data = book,
                    Successfull = true
                };
            }
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {

            BoardGame? book = await _dbContext.BoardGames.FindAsync(id);

            if (book == null)
            {
                return new ResponseData<string>
                {
                    Successfull = false,
                    ErrorMessage = "No item found"
                };
            }

            var host = "https://" + _httpContextAccessor.HttpContext.Request.Host;
            var imagesFolder = Path.Combine(_env.WebRootPath, "images");
            if (formFile != null)
            {
                // Удалить предыдущее изображение
                if (!String.IsNullOrEmpty(book.Image))
                {
                    var prevImage = Path.Combine(imagesFolder, Path.GetFileName(book.Image));
                    if (File.Exists(prevImage))
                    {
                        File.Delete(prevImage);
                    }
                }

                // Создать имя файла
                var ext = Path.GetExtension(formFile.FileName);
                var newImageName = Path.ChangeExtension(Path.GetRandomFileName(), ext);
                // Сохранить файл
                var newImagePath = Path.Combine(imagesFolder, newImageName);
                using (Stream fileStream = new FileStream(newImagePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                // Указать имя файла в объекте
                book.Image = $"{host}/images/{newImageName}";
                await _dbContext.SaveChangesAsync();
            }
            return new ResponseData<string>
            {
                Data = book.Image,
                Successfull = true
            };
        }

        public async Task UpdateBoardGameAsync(int id, BoardGame book, IFormFile? formFile)
        {
            BoardGame? bookToUpdate = await _dbContext.BoardGames.FindAsync(id);

            if (bookToUpdate != null)
            {
                bookToUpdate.Name = book.Name;
                bookToUpdate.Description = book.Description;
                bookToUpdate.Price = book.Price;
                bookToUpdate.CategoryId = book.CategoryId;

                if (formFile != null)
                {
                    bookToUpdate.Image = (await SaveImageAsync(id, formFile)).Data;
                }

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
