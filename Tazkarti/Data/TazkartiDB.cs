using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tazkarti.Models;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.Entertainment;
using Tazkarti.Models.Sport;
namespace Tazkarti.Data
{
	public class TazkartiDB : IdentityDbContext<ApplicationUser>
	{
		public TazkartiDB (DbContextOptions<TazkartiDB> options) :base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EventTicket>()
				.HasKey(k => k.TicketId);
			modelBuilder.Entity<MatchTeams>()
				.HasKey(k => new { k.MatchId,k.TeamId });
			modelBuilder.Entity<MatchTicket>()
				.HasKey(k => k.TicketId);
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Category>Categories { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<EventTicket> EventsTickets { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<MatchTeams> MatchesTeams { get; set; }
        public DbSet<MatchTicket> MatchesTickets { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

	}
}
