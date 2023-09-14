using PokemonReviewApp.Models;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Data;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        bool ICountryRepository.CountryExists(int id)
        {
            return _context.Countries.Any(p => p.Id == id);
        }

        ICollection<Country> ICountryRepository.GetCountries()
        {
            return _context.Countries.ToList();
        }

        Country ICountryRepository.GetCountry(int id)
        {
            return _context.Countries.Where(p => 
                p.Id == id).FirstOrDefault();
        }

        Country ICountryRepository.GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(p =>
                p.Id == ownerId).Select(p => 
                p.Country).FirstOrDefault();
        }

        ICollection<Owner> ICountryRepository.GetOwnersFromCountry(int countryId)
        {
            return _context.Owners.Where(p =>
                p.Country.Id == countryId).ToList();
        }
    }
}
