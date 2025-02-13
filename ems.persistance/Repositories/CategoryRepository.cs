﻿using ems.application.DTOs.CategoryDTO;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private WorkforceDbContext _context;
        private ILogger _logger;


        public CategoryRepository(WorkforceDbContext context, ILogger<CategoryRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }
        public async Task<IEnumerable<Category>> GetAllMainCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all main categories where ParentCategoryId is NULL.");

                var mainCategories = await _context.Categories
                    .Where(c => c.ParentId == null && c.IsActive)// Using AutoMapper projection
                    .ToListAsync();

                return mainCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching main categories.");
                throw;
            }
        }
        //public Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO category)
        //{
        //    public async Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO category)
        //    {
        //        try
        //        {
        //            var newCategory = new Category
        //            {
        //                Name = category.Name,
        //                Description = category.Description,
        //                IsActive = true,
        //                AddedById = category.AddedById,
        //                AddedDateTime = DateTime.UtcNow
        //            };

        //            await _context.Categories.AddAsync(newCategory);
        //            await _context.SaveChangesAsync();

        //            return new CategoryResponseDTO
        //            {
        //                Id = newCategory.Id,
        //                Name = newCategory.Name,
        //                Description = newCategory.Description,
        //                IsActive = newCategory.IsActive
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error while adding category: {CategoryName}", category.Name);
        //            throw new ApplicationException("An error occurred while adding the category.");
        //        }
        //    }


        //}

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
 

      

        public Task<IEnumerable<CategoryResponseDTO>> GetAllMainCategoriesAsync(CategoryResponseDTO categoryResponseDTO )
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<CategoryResponseDTO>> ICategoryRepository.GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        

        Task<CategoryResponseDTO> ICategoryRepository.GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<CategoryResponseDTO> ICategoryRepository.UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<Category>> GetAllChildCategoryByIdAsync(long requestId, int categoryParentId)
        {
            try
            {
                _logger.LogInformation("Fetching categories based on request conditions.");
                var mainCategories = await _context.Categories.Where(c => c.ParentId == categoryParentId && c.IsActive).ToListAsync();

                _logger.LogInformation("Filtering by ParentCategoryId: {ParentCategoryId}", categoryParentId);
                
                 

                return mainCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching child categories.");
                throw;
            }
        }

        public Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
