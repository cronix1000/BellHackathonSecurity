using BelLHackathonSecurity.Data;
using BelLHackathonSecurity.Models;

namespace BelLHackathonSecurity
{
    public class DataManager : IDataManager
    {
        private readonly ApplicationDbContext _context;

        public DataManager(ApplicationDbContext applicationDbContext) {
            _context = applicationDbContext;
        }

        public async Task<int> CreateDataObject(string id, string username)
        {
            UserData userData = new UserData()
            {
                Id = Guid.NewGuid(),
                UserId = id,
                Username = username
            };

            await _context.userDatas.AddAsync(userData);
            await _context.SaveChangesAsync();
            return 0;
        }
    }
}
