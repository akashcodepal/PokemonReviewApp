using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
  public class ReviewerRepository: IReviewerRepository
  {
    private readonly DataContext _context;
    public ReviewerRepository(DataContext context)
    {
      _context = context;
    }

    public Reviewer GetReviewer(int id)
    {
      return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
    }

    public ICollection<Reviewer> GetReviewers()
    {
      return _context.Reviewers.ToList();
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
      return _context.Reviewers.Where(r => r.Id == reviewerId).SelectMany(r => r.Reviews).ToList();
    }

    public bool ReviewExists(int id)
    {
      return _context.Reviewers.Any(r => r.Id == id);
    }
  }
}