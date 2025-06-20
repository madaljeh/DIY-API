using DIY_API.DTOs.Category;
using DIY_API.Helper;
using DIY_API.Interfaces;
using DIY_API.Models;
using EmailServicePackage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DIY_API.Services
{
    public class CategoryService : ICategories
    {
        private readonly DIYDbContext _diycontext;
        
        public CategoryService(DIYDbContext context)
        {
            _diycontext = context;
           
        }
        public async Task<CategoryDTO> AddNewCategory(NewCategoryDTO input)
        {
            var addnew = new Category
            {
                CategoryNameAr = input.CategoryNameAr,
                CategoryNameEn = input.CategoryNameEn,
                Image = input.Image,
                CreatedBy = input.CreatedBy,
                UpdatedBy = input.UpdatedBy,
                CreationDate = input.CreationDate,
                UpdateDate = input.UpdateDate,
                IsActive = input.IsActive
            };
            _diycontext.Categories.Add(addnew);
            await _diycontext.SaveChangesAsync();
            return new CategoryDTO
            {
                CategoryId = addnew.CategoryId,
                CategoryNameAr = addnew.CategoryNameAr,
                CategoryNameEn = addnew.CategoryNameEn,
                Image = addnew.Image,
                CreatedBy = addnew.CreatedBy,
            };
        }

        public async Task<bool> DeleteCategory(int Id)
        {
            var existing = await _diycontext.Categories.FindAsync(Id);
            if (existing is null)
                return false;

            _diycontext.Categories.Remove(existing);
            await _diycontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryDTO>> GetAllCategory(PaginationParameters pagination)
        {
            return await _diycontext.Categories
                .Where(x => x.IsActive == true)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(x => new CategoryDTO
                {
                    CategoryId = x.CategoryId,
                    CategoryNameAr = x.CategoryNameAr,
                    CategoryNameEn = x.CategoryNameEn,
                    Image = x.Image,
                    CreatedBy = x.CreatedBy,
                }).ToListAsync();
        }

        public async Task<CategoryDetailsDTO> GetCategoryById(int Id)
        {
            var Category =await _diycontext.Categories.Where(x => x.CategoryId == Id).Select(x => new CategoryDetailsDTO
            {
                CategoryNameAr = x.CategoryNameAr,
                CategoryNameEn = x.CategoryNameEn,
                Image = x.Image,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy,
                CreationDate = x.CreationDate,
                UpdateDate = x.UpdateDate,
                IsActive = x.IsActive
            }).FirstOrDefaultAsync();

            return Category;
        }

        public async Task<CategoryDTO> UpdateCategory(UpdateCategoryDTO input)
        {
            var Category = _diycontext.Categories.Where(x => x.CategoryId == input.CategoryId).FirstOrDefault();
            if (Category == null)
            {
                return null;
            }
            Category.CategoryNameAr = input.CategoryNameAr;
            Category.CategoryNameEn = input.CategoryNameEn;
            Category.UpdateDate = input.UpdateDate;
            Category.CreationDate = input.CreationDate;
            Category.UpdatedBy = input.UpdatedBy;
            Category.CreatedBy = input.CreatedBy;
            Category.IsActive = input.IsActive;
            Category.Image = input.Image;
            _diycontext.Categories.Update(Category);
            await _diycontext.SaveChangesAsync();

            return new CategoryDTO
            {
                CategoryId = Category.CategoryId,
                CategoryNameAr = Category.CategoryNameAr,
                CategoryNameEn = Category.CategoryNameEn,
                Image = Category.Image,
                CreatedBy = Category.CreatedBy,
            };
        }
    }
}
