namespace Tazkarti.Models.Entertainment
{
	public class Event
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }
		public int AllowedNumberOfPeople { get; set; }
		public string? Location { get; set; }
		public int NumberOfShows { get; set; }
		public int CateogryId { get; set; }

		public Category Category { get; set; }

	}
}
