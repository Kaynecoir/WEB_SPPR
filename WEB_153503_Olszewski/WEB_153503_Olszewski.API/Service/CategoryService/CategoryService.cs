using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.API.Data;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.API.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var data = await _dbContext.Categories.ToListAsync();
            return new ResponseData<List<Category>>()
            {
                Data = data,
                Success = true
            };
        }
    }
}
