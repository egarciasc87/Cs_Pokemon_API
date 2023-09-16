using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("apl/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            var owners = _ownerRepository.GetOwners();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        public IActionResult GetOwner(int ownerId)
        {
            var owner = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/Pokemon")]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            var pokemons = _ownerRepository.GetPokemonsByOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{ownerId}/exists")]
        public IActionResult OwnerExists(int ownerId)
        {
            var result = _ownerRepository.OwnerExists(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }
    }
}
