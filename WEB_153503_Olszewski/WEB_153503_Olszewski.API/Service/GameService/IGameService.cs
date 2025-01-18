using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.API.Service.GameService
{
    public interface IGameService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">нормализованное имя категории для фильтрации</param>
        /// <param name="pageNo">номер страницы списка</param>
        /// <param name="pageSize">количество объектов на странице</param>
        /// <returns></returns>
        public Task<ResponseData<GameListModel<BoardGame>>> GetBoardGameListAsync(string? categoryNormalizedName, 
                                                                                  int pageNo = 1, 
                                                                                  int pageSize = 3);

        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns></returns>
        public Task<ResponseData<BoardGame>> GetBoardGameByIdAsync(int id);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="id">Id изменяемомго объекта</param>
        /// <param name="product">объект с новыми параметрами</param>
        /// <returns></returns>
        public Task UpdateBoardGameAsync(int id, BoardGame book, IFormFile? formFile);

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id удаляемомго объекта</param>
        /// <returns></returns>
        public Task DeleteBoardGameAsync(int id);

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="product">Новый объект</param>
        /// <returns>Созданный объект</returns>
        public Task<ResponseData<BoardGame>> CreateBoardGameAsync(BoardGame book, IFormFile? formFile);

        /// <summary>
        /// Сохранить файл изображения для объекта
        /// </summary>
        /// <param name="id">Id объекта</param>
        /// <param name="formFile">файл изображения</param>
        /// <returns>Url к файлу изображения</returns>
        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }
}
