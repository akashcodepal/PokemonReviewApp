using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
  public class OwnerRepository : IOwnerRepository
  {

    private readonly DataContext _context;
    public OwnerRepository(DataContext context)
    {
      _context = context;
    }

    public bool OwnerExists(int id)
    {
      return _context.Owners.Any(o => o.Id == id);
    }

    public Owner GetOwner(int id)
    {
      return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
    }

    public ICollection<Owner> GetOwners()
    {
      return _context.Owners.ToList();
    }

    public ICollection<Owner> GetOwnersOfPokemon(int pokeId)
    {
      return _context.PokemonOwners.Where(po => po.PokemonId == pokeId).Select(o => o.Owner).ToList();
    }

    public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
    {
      return _context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
    }

    public bool CreateOwner(Owner owner)
    {
      _context.Add(owner);
      return Save();
    }

    public bool Save()
    {
      return _context.SaveChanges() > 0 ? true : false;
    }

    public bool OwnerExistsByName(string ownerName)
    {
      return _context.Owners.Any(c => (c.FirstName + " " + c.LastName).Trim().ToUpper() == ownerName.Trim().ToUpper());
    }

    public bool UpdateOwner(Owner owner)
    {
      _context.Update(owner);
      return Save();
    }
  }
}