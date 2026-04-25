namespace PokemonReviewApp.Dto
{
  public class ReviewDto
  {
    public int id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    // Foreign Keys
    public int ReviewerId { get; set; }
    public int PokemonId { get; set; }
  }
}