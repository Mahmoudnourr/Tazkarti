using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tazkarti.Migrations
{
    /// <inheritdoc />
    public partial class sc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchesTickets",
                table: "MatchesTickets");

            migrationBuilder.DropIndex(
                name: "IX_MatchesTickets_TicketId",
                table: "MatchesTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsTickets",
                table: "EventsTickets");

            migrationBuilder.DropIndex(
                name: "IX_EventsTickets_TicketId",
                table: "EventsTickets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchesTickets",
                table: "MatchesTickets",
                column: "TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsTickets",
                table: "EventsTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesTickets_MatchId",
                table: "MatchesTickets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsTickets_EventId",
                table: "EventsTickets",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchesTickets",
                table: "MatchesTickets");

            migrationBuilder.DropIndex(
                name: "IX_MatchesTickets_MatchId",
                table: "MatchesTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsTickets",
                table: "EventsTickets");

            migrationBuilder.DropIndex(
                name: "IX_EventsTickets_EventId",
                table: "EventsTickets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchesTickets",
                table: "MatchesTickets",
                columns: new[] { "MatchId", "TicketId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsTickets",
                table: "EventsTickets",
                columns: new[] { "EventId", "TicketId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchesTickets_TicketId",
                table: "MatchesTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsTickets_TicketId",
                table: "EventsTickets",
                column: "TicketId");
        }
    }
}
