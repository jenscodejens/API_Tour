using System.ComponentModel.DataAnnotations;

namespace Tournament_Core.Entities
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
