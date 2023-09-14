﻿using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Data;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        bool ICategoryRepository.CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        ICollection<Category> ICategoryRepository.GetCategories()
        {
            return _context.Categories.ToList();
        }

        Category ICategoryRepository.GetCategory(int id)
        {
            return _context.Categories.Where(p => p.Id == id).FirstOrDefault();
        }

        ICollection<Pokemon> ICategoryRepository.GetPokemonsByCategory(
            int categoryId)
        {
            return _context.PokemonCategories.Where(p => 
                p.CategoryId == categoryId).Select(p => 
                p.Pokemon).ToList();
        }
    }
}