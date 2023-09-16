using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(p => 
                p.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewByPokemon(
            int pokemonId)
        {
            return _context.Reviews.Where(p =>
                p.Pokemon.Id == pokemonId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(p => 
                p.Id == reviewId);
        }
    }
}
