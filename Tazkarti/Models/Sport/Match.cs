namespace Tazkarti.Models.Sport
{
	public class Match
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Stage { get; set; }
		public int AllowedNumberOfPeople { get; set; }
		public string? Tournament {  get; set; }
		public string? Stadium {  get; set; }
		public DateTime? Date { get; set; } 
		public string? Location { get; set; }
		public string? Governate { get; set; }

	}
}
