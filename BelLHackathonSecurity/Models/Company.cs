namespace BelLHackathonSecurity.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyLink { get; set; }
        public byte[]? CompanyImage { get; set; }
    }
}
