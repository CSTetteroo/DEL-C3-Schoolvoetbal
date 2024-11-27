namespace VoetbalTournamentAPI.Model
{
    public class Match
    {
        public int Id { get; set; }

        public string Team1_id { get; set; }

        public string Team2_id { get; set; }

        public int Team1_score { get; set; }

        public int Team2_score { get; set; }

        public string Field { get; set; }

        public int referee_id { get; set; }

        public string time { get; set; }


      
    }
}
