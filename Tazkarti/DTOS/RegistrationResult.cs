namespace Tazkarti.DTOS
{
    public class RegistrationResult
    {
        public bool IsAuthorized { get; set; }
        public List<String> Errors { get; set; }

    }
}
