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

    public bool CountryExistsByName(string name)
    {
      return _dataContext.Countries.Any(c => c.Name.Trim().ToUpper() == name.Trim().ToUpper());
    }

    public bool CreateCountry(Country country)
    {
      _dataContext.Add(country);
      return Save();
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

    public bool Save()
    {
      var savedChanges = _dataContext.SaveChanges();
      return savedChanges > 0 ? true : false;
    }
  }
}