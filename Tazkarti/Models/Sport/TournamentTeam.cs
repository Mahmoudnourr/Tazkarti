namespace Tazkarti.Models.Sport
{
    public class TournamentTeam
    {
        public Tournament Tournament { get; set; }
        public Team Team { get; set; }

        public int TeamId { get; set; }
        public int TournamentId { get; set; }
    }
}
