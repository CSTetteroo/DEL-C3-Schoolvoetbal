using Microsoft.EntityFrameworkCore;
using VoetbalTournamentAPI.Model;

namespace VoetbalTournamentAPI.Data
{
    public class VoetbalTournamentDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;" +                     // Server name
                "port=3306;" +                            // Server port
                "user=root;" +                            // Username
                "password=;" +                            // Password
                "database=voetbal_tournament;"            // Database name
                , Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql") // Version
            );
        }
    }
}
