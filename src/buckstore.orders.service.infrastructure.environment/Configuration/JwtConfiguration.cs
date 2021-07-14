namespace buckstore.orders.service.infrastructure.environment.Configuration
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public string TokenIssuer { get; set; }
        public string Audience { get; set; }
    }
}