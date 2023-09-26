using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("apl/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemon = _pokemonRepository.GetPokemons();
            var mappedPokemonDto = _mapper.Map<List<PokemonDto>>(pokemon);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(mappedPokemonDto);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var pokemon = _pokemonRepository.GetPokemon(pokeId);
            var mappedPokemonDto = _mapper.Map<PokemonDto>(pokemon);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(mappedPokemonDto);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

        [HttpPost]
        public IActionResult CreatePokemon(
           [FromQuery] int ownerId,
           [FromQuery] int categoryId,
           [FromBody] PokemonDto pokemon)
        {
            if (pokemon == null)
                return BadRequest(ModelState);

            var response = _pokemonRepository.GetPokemons().
                Where(p => p.Name.ToUpper() == pokemon.Name.ToUpper()).
                FirstOrDefault();

            if (response != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemon);

            if (!_pokemonRepository.CreatePokemon(pokemonMap, 
                categoryId,
                ownerId))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
