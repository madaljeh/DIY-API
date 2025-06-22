using DIY_API.DTOs.Authantication;
using DIY_API.DTOs.Category;
using DIY_API.Helper;

namespace DIY_API.Interfaces
{
    public interface ICategories
    {
        Task<List<CategoryDTO>> GetAllCategory(PaginationParameters pagination);
        Task<CategoryDetailsDTO> GetCategoryById(int Id);
        Task<CategoryDTO> AddNewCategory(NewCategoryDTO input);
        Task<CategoryDTO> UpdateCategory(UpdateCategoryDTO input);
        Task<bool> DeleteCategory(int Id);
        Task<bool> CategoryActivationStatus(int CategoryId, CategoryActivationDTO input);
    }
}
