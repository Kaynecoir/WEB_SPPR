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
            List<Category> catList = new List<Category>
            {
                new Category { Name = "Карточные игры", NormalizedName = "card_game" },
                new Category { Name = "Головоломки", NormalizedName = "mindbreak_game" },
                new Category { Name = "Настольные игры", NormalizedName = "board_game" },
                new Category { Name = "Ролевые игры", NormalizedName = "role_game" },
            };

            await context.Categories.AddRangeAsync(catList);
            await context.SaveChangesAsync();


            string imageRoot = $"{app.Configuration["AppUrl"]!}/images";

            var _boardGames = new List<BoardGame>
            {
                new BoardGame {Name="Dungeon and Dragons",
                    Description="Игра в стиле фэнтези.", Price = 191.0f, Image="img/dnd.jpg",
                    Category=await context.Categories.SingleAsync(c=>c.NormalizedName.Equals("role_game"))},
                new BoardGame {Name="Vampire: the Masquarade",
                    Description="Игра в стиле темного фэнтези.", Price = 181.0f, Image="img/vtm.jpg",
                    Category=await context.Categories.SingleAsync(c => c.NormalizedName.Equals("role_game"))},
                new BoardGame {Name="Pathfinder",
                    Description="Игра в стиле фэнтези.", Price = 171.0f, Image="img/path.jpg",
                    Category=await context.Categories.SingleAsync(c => c.NormalizedName.Equals("role_game"))},
                new BoardGame {Name="Catan",
                    Description="Колонизаторы.", Price = 141.0f, Image="img/catan.jpg",
                    Category=await context.Categories.SingleAsync(c => c.NormalizedName.Equals("board_game"))},
                new BoardGame {Name="Citadel",
                    Description="Цитадель.", Price = 71.0f, Image="img/citadel.jpg",
                    Category=await context.Categories.SingleAsync(c => c.NormalizedName.Equals("board_game"))},
                new BoardGame {Name="Bang!",
                    Description="Бэнг!", Price = 51.0f, Image="img/bang.jpg",
                    Category=await context.Categories.SingleAsync(c => c.NormalizedName.Equals("board_game"))},
                new BoardGame {Name="Dungeon and Dragons V2",
                    Description="Игра в стиле фэнтези.", Price = 191.0f, Image="img/dnd.jpg",
                    Category=await context.Categories.SingleAsync(c => c.NormalizedName.Equals("role_game"))},
            };

            await context.BoardGames.AddRangeAsync(_boardGames);
            await context.SaveChangesAsync();
        }
    }
}
