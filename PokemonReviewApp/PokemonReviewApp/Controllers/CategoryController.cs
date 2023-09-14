using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("apl/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //Get categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        //Get category
        [HttpGet("{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        //Get pokemos in category
        [HttpGet("pokemon/{categoryId}")]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            var pokemons = _categoryRepository.GetPokemonsByCategory(
                categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}
