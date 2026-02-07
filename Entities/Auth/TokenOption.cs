namespace ProductApi.Entities.Auth
{
    public class TokenOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccesTokenExpiration { get; set; }
        public string SecurityKey { get; set; }

    }
}
