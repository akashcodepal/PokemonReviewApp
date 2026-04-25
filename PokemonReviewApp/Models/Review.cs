namespace PokemonReviewApp.Models
{
    public class Review
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int Rating { get; set; }

        // Foreign Keys
        public int ReviewerId {get; set;}
        public int PokemonId {get; set;}

        // A Review can be written by Single Reviewer => one to one relationship
        public Reviewer Reviewer { get; set; }

        // A Review can be written for Single Pokemon => one to one relationship
        public Pokemon Pokemon { get; set; }

    }
}
