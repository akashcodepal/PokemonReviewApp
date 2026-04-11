using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
  public class CountryRepository : ICountryRepository
  {
    private readonly DataContext _dataContext;
    public CountryRepository(DataContext dataContext)
    {
      _dataContext = dataContext;
    }

    public bool CountryExists(int countryId)
    {
      return _dataContext.Countries.Any(c => c.id == countryId);
    }

    public ICollection<Country> GetCountries()
    {
      return _dataContext.Countries.ToList();
    }

    public Country GetCountry(int id)
    {
      return _dataContext.Countries.Where(c => c.id == id).FirstOrDefault();
    }

    public Country GetCountryByOwner(int ownerId)
    {
      return _dataContext.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnersFromCountry(int countryId)
    {
      return _dataContext.Owners.Where(o => o.Country.id == countryId).ToList();
    }
  }
}