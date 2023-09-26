using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("apl/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(
            IReviewerRepository reviewerRepository,
            IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;   
        }

        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewers = _reviewerRepository.GetReviewers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}/getreviewer")]
        public IActionResult GetReviewer(int reviewerId)
        {
            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/getreviewsbyreviewer")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var reviews = _reviewerRepository.GetReviewsByReviewer(
                reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewerId}/reviewerexists")]
        public IActionResult ReviewerExists(int reviewerId)
        {
            var result = _reviewerRepository.ReviewerExists(
                reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateReview(
           [FromBody] ReviewerDto reviewer)
        {
            if (reviewer == null)
                return BadRequest(ModelState);

            var response = _reviewerRepository.GetReviewers().
                Where(p => p.LastName.ToUpper() == reviewer.LastName.ToUpper()).
                FirstOrDefault();

            if (response != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            var reviewerMap = _mapper.Map<Reviewer>(reviewer);

            if (!_reviewerRepository.CreateReviewer(
                reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
