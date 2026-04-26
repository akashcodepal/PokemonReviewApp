 using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
  public interface IOwnerRepository
  {
    ICollection<Owner> GetOwners();
    Owner GetOwner(int id);
    ICollection<Owner> GetOwnersOfPokemon(int pokeId);
    ICollection<Pokemon> GetPokemonByOwner(int ownerId);
    bool OwnerExists(int ownerId);
    bool OwnerExistsByName(string ownerName);

    bool CreateOwner(Owner owner);
    bool UpdateOwner(Owner owner);

    bool Save();

  }
}