using System.ComponentModel.DataAnnotations;

namespace Tournament_Core.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
    }
}
