namespace Infrastructure.Repositories.Authentication
{
    public class Jwt
    {
        public const string SectionName = "Jwt";
        public string securityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
