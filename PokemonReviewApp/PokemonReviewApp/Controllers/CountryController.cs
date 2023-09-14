using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("apl/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("countryId")]
        public IActionResult GetCountry(int countryId)
        {
            var country = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("ownerId")]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var owner = _countryRepository.GetCountryByOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        //[HttpGet("countryId")]
        //public IActionResult GetOwnersFromCountry(int countryId)
        //{
        //    var country = _countryRepository.GetOwnersFromCountry(countryId);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(country);
        //}
    }
}
