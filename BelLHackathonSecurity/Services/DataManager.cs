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

        public async Task<int> CreateDataObject(string email)
        {
            if(_context.userDatas.Any(a => a.Email == email)){
                return 1;
            }

            UserData userData = new UserData()
            {
                Id = Guid.NewGuid(),
                Email = email,
            };

            await _context.userDatas.AddAsync(userData);
            await _context.SaveChangesAsync();
            return 0;
        }
    }
}
