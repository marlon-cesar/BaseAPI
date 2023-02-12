namespace  BaseAPI.API.Auth
{
    public class AuthRequestDto
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public string RefreshToken { get; set; }

    }
}
