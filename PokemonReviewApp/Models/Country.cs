namespace PokemonReviewApp.Models
{
    public class Country
    {
        public int id { get; set; }
        public string Name { get; set; }

        // A Country can have multiple Owners => one to Many relationship
        public ICollection<Owner> Owners { get; set; }
    }
}
