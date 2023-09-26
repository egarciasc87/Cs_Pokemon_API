using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewByPokemon(int pokemonId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review,
            int pokemonId);
    }
}
