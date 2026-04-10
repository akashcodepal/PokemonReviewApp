namespace PokemonReviewApp.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Reviewer can write multiple reviews => One to Many Relationship
        public ICollection<Review> Reviews { get; set; }
    }
}
