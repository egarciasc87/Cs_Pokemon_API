using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(p => 
                p.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(
            int reviewerId)
        {
            return _context.Reviews.Where(p =>
                p.Reviewer.Id == reviewerId).ToList();
        }

        bool IReviewerRepository.ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(p=>p.Id == reviewerId);
        }
    }
}
