namespace AulaAuth02.Settings
{
    public class JwtSettings
    {
        public const string Secao = "Jwt";

        public string Key { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public int ExpiracaoMinutos { get; set; }

        
    }
}
