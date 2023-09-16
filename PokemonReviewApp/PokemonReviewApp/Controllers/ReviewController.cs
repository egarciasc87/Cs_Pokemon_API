using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("apl/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet("{reviewId}/getreview")]
        public IActionResult GetReview(int reviewId)
        {
            var review = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("{pokemonId}/getpokemons")]
        public IActionResult GetReviewByPokemon(int pokemonId)
        {
            var pokemons = _reviewRepository.GetReviewByPokemon(
                pokemonId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}/exists")]
        public IActionResult ReviewExists(int reviewId)
        {
            var result = _reviewRepository.ReviewExists(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }
    }
}
