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
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;   
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

        [HttpPost]
        public IActionResult CreateCountry([FromBody] 
            CountryDto country)
        {
            if (country == null)
                return BadRequest(ModelState);

            var response = _countryRepository.GetCountries().
                Where(p => p.Name.ToUpper() == country.Name.ToUpper()).
                FirstOrDefault();

            if (response != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(country);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
