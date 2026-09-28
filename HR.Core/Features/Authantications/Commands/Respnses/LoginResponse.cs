namespace HR.Core.Features.Authantications.Commands.Respnses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}