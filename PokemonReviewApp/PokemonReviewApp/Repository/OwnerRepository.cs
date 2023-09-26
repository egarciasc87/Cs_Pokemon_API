using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Data;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(p => 
                p.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(
            int ownerId)
        {
            return _context.PokemonOwners.Where(p => 
                p.OwnerId == ownerId).Select(p=>
                p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(p => p.Id == ownerId);
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
