using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(Pokemon pokemon, int ownerId, int categoryId)
        {
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var owner = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            
            var PokemonCategory = new PokemonCategory()
            {
                Pokemon = pokemon,
                Category = category,
            };

            var PokemonOwner = new PokemonOwner()
            {
                Pokemon = pokemon,
                Owner = owner,
            };
            
            _context.Add(PokemonCategory);
            _context.Add(PokemonOwner);
            _context.Add(pokemon);

            return Save();

        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (reviews.Count() == 0)
            {
                return 0;
            }

            var avg = (decimal)reviews.Sum(s => s.Rating) / reviews.Count();

            return avg;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon
            .OrderBy(p => p.Id)
            .Include(p => p.PokemonOwners)
            .ThenInclude(p => p.Owner)
            .Include(p => p.PokemonCategories)
            .ThenInclude(p => p.Category)
            .ToList();
        }

        public Pokemon GetPokenmon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();

        }

        public bool PokemonExists(int pokeId)
        {
            //return _context.Pokemon.Where(p => p.Id == pokeId).Count() > 0; 
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }

        public bool PokemonExistsByName(string pokemonName)
        {
            return _context.Pokemon.Any(c => c.Name.Trim().ToUpper() == pokemonName.Trim().ToUpper());
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon, int ownerId, int categoryId)
        {
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var owner = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            
            var PokemonCategory = new PokemonCategory()
            {
                Pokemon = pokemon,
                Category = category,
            };

            var PokemonOwner = new PokemonOwner()
            {
                Pokemon = pokemon,
                Owner = owner,
            };
            
            _context.Update(PokemonCategory);
            _context.Update(PokemonOwner);
            _context.Update(pokemon);

            return Save();

        }
    }
}
