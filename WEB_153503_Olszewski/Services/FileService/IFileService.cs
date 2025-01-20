using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.Services.FileService
{
    public interface IFileService
    {
        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="formFile">Файл, переданный формой</param>
        /// <returns>URL сохраненного файла</returns>
        Task<string> SaveFileAsync(IFormFile formFile);
        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        Task DeleteFileAsync(string fileName);
    }
}
