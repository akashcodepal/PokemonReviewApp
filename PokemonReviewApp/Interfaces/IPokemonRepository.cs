using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokenmon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeId);
        bool PokemonExistsByName(string pokemonName);
        bool CreatePokemon(Pokemon pokemon, int ownerId, int categoryId);
        bool Save();
    }
}
