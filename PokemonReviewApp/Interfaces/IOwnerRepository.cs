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

  }
}