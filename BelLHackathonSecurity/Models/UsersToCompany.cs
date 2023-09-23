namespace BelLHackathonSecurity.Models
{
    public class UsersToCompany
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
