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

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            
            if(reviews.Count() == 0)
            {
                return 0;
            }

            var avg = (decimal)reviews.Sum(s => s.Rating)/reviews.Count();

            return avg;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
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
    }
}
