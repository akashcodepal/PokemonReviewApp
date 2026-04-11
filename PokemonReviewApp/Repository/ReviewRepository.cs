using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
  public class ReviewRepository: IReviewRepository
  {
    private readonly DataContext _context;
    public ReviewRepository(DataContext context)
    {
      _context = context;
    }

    public Review GetReview(int id)
    {
      return _context.Reviews.Where(r => r.id == id).FirstOrDefault();
    }

    public ICollection<Review> GetReviews()
    {
      return _context.Reviews.ToList();
    }

    public ICollection<Review> GetReviewsOfPokemon(int pokeId)
    {
      return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
    }

    public bool ReviewExists(int id)
    {
      return _context.Reviews.Any(r => r.id == id);
    }
  }
}