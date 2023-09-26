using PokemonReviewApp.Models;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            decimal average = 0;
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id == id);

            if (reviews.Count() <= 0)
            {
                return average;
            }
            else
            {
                average = reviews.Sum(p => p.Rating) / reviews.Count();
                return average;
            }
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(p => p.Id == id);
        }

        public bool CreatePokemon(Pokemon pokemon,
            int categoryId,
            int ownerId)
        {
            var pokemonOwnerEntity = _context.Owners.
                Where(p => p.Id == ownerId).
                FirstOrDefault();
            var category = _context.Categories.
                Where(p => p.Id == categoryId).
                FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);
            _context.Add(pokemon);

            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
