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

        public ReviewerController(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
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
    }
}
