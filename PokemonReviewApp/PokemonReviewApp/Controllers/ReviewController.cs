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
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(
            IReviewRepository reviewRepository,
            IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository,
            IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
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

        [HttpPost]
        public IActionResult CreateReview(
            [FromQuery] int pokemonId,
            [FromQuery] int reviewerId,
           [FromBody] ReviewDto review)
        {
            if (review == null)
                return BadRequest(ModelState);

            var response = _reviewRepository.GetReviews().
                Where(p => p.Title.ToUpper() == review.Title.ToUpper()).
                FirstOrDefault();

            if (response != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            var reviewMap = _mapper.Map<Review>(review);
            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if (!_reviewRepository.CreateReview(reviewMap,
                pokemonId))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        public IActionResult UpdateReviewer(int reviewId,
            [FromBody] ReviewDto review)
        {
            if (review == null)
                return BadRequest(ModelState);

            if (reviewId != review.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(review);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.TryAddModelError("",
                    "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewId))
                return NotFound();

            var reviewMap = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewMap))
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting review");
            }
                        
            return NoContent();
        }
    }
}
