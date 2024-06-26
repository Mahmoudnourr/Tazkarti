namespace Tazkarti.Models.Entertainment
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }
		public ICollection<Event> Events { get; set; }

	}
}
