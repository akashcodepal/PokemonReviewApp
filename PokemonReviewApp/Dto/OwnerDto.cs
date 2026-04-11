using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto
{
  public class OwnerDto
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Gym { get; set; }

    // A Owner can have 1 Country => one to one relationship
    public Country Country { get; set; }
  }
}