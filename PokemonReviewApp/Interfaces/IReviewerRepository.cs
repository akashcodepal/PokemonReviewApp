using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
  public interface IReviewerRepository
  {
    ICollection<Reviewer> GetReviewers();
    Reviewer GetReviewer(int id);
    ICollection<Review> GetReviewsByReviewer(int reviewerId);
    bool ReviewExists(int id);
    bool ReviewExistsByName(string name);
    bool CreateReviewer(Reviewer reviewer);
    bool Save();
  }
}