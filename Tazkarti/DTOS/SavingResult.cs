namespace Tazkarti.DTOS
{
	public class SavingResult
	{
		public bool Success { get; set; }
		public Dictionary<string, string>? Errors { get; set; }
	}
}