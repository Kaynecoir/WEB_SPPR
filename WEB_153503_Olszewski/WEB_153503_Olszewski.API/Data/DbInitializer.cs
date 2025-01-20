using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace WEB_153503_Olszewski.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Выполнение миграций
            await context.Database.MigrateAsync();

            context.BoardGames.RemoveRange(context.BoardGames.Where(b => true));
            await context.SaveChangesAsync();

            // создаем объекты Category
            List<Category> _categoryList = new List<Category>
            {
                new Category { Name = "Карточные игры", NormalizedName = "card_game" },
                new Category { Name = "Головоломки", NormalizedName = "mindbreak_game" },
                new Category { Name = "Настольные игры", NormalizedName = "board_game" },
                new Category { Name = "Ролевые игры", NormalizedName = "role_game" },
            };

            await context.Categories.AddRangeAsync(_categoryList);
            await context.SaveChangesAsync();


            string imageRoot = $"{app.Configuration["AppUri"]!}images";

            List<BoardGame> _boardGamesList = new List<BoardGame>
            {
                new BoardGame
                {
                    Id = 1,
                    Name="Dungeon and Dragons",
                    Description="Игра в стиле фэнтези.",
                    Price = 191.0f, 
                    Image=$"{imageRoot!}/dnd.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("role_game"))
                },
                new BoardGame
                {
                    Id = 2,
                    Name="Vampire: the Masquarade",
                    Description="Игра в стиле темного фэнтези.",
                    Price = 181.0f, 
                    Image=$"{imageRoot!}/vtm.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("role_game"))
                },
                new BoardGame
                {
                    Id = 3,
                    Name="Pathfinder",
                    Description="Игра в стиле фэнтези.",
                    Price = 171.0f, 
                    Image=$"{imageRoot!}/path.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("role_game"))
                },
                new BoardGame
                {
                    Id = 4,
                    Name="Catan",
                    Description="Колонизаторы.",
                    Price = 141.0f, 
                    Image=$"{imageRoot!}/catan.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 5,
                    Name="Citadel",
                    Description="Цитадель.",
                    Price = 71.0f, 
                    Image=$"{imageRoot!}/citadel.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 6,
                    Name="Bang!",
                    Description="Бэнг!",
                    Price = 51.0f, 
                    Image=$"{imageRoot!}/bang.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 7,
                    Name="Dungeon and Dragons V2",
                    Description="Игра в стиле фэнтези.",
                    Price = 191.0f, 
                    Image=$"{imageRoot!}/dnd.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("role_game"))
                },
                new BoardGame
                {
                    Id = 8,
                    Name="Bang! Star Wars",
                    Description="Бэнг по Звездным Войнам!",
                    Price = 71.0f, 
                    Image=$"{imageRoot!}/bang_sw.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 9,
                    Name="Carcassonne",
                    Description="Каркассоны! Средневековье и тайлы!",
                    Price = 71.0f, 
                    Image=$"{imageRoot!}/carcassonne.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("board_game"))
                },
                new BoardGame
                {
                    Id = 10,
                    Name="UNO",
                    Description="Крикни UNO раньше других!",
                    Price = 71.0f, 
                    Image=$"{imageRoot!}/uno.jpg",
                    Mime="JPEG",
                    Category=_categoryList.Find(c=>c.NormalizedName.Equals("card_game"))
                }
            };

            await context.BoardGames.AddRangeAsync(_boardGamesList);
            await context.SaveChangesAsync();
        }
    }
}
