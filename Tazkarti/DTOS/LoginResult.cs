namespace Tazkarti.DTOS
{
    public class LoginResult
    {
        public string UserName { get; set; }
        public bool Success { get; set; }
        public List<String> Errors { get; set; }
    }
}
