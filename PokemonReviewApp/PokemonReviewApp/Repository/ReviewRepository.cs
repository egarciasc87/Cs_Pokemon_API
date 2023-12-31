﻿using PokemonReviewApp.Data;
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

        public bool CreateReview(Review review,
            int pokemonId)
        {
            _context.Add(review);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            var saved = _context.SaveChanges();
            return true;
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            var saved = _context.SaveChanges();
            return true;
        }
    }
}
