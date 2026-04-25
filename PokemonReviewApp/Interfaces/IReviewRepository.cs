using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
  public interface IReviewRepository
  {
    ICollection<Review> GetReviews();
    Review GetReview(int id);
    ICollection<Review> GetReviewsOfPokemon(int pokeId);
    bool ReviewExists(int id); 
    bool CreateReview(Review review);

    bool Save();
  }
}