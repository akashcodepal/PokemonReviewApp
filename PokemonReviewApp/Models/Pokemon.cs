namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        // A Pokemon can have multiple Reviews => one to Many relationship
        public ICollection<Review> Reviews { get;set; }

        // Many to Many
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
 