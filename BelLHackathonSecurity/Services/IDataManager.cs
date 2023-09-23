using Microsoft.AspNetCore.Mvc;

namespace BelLHackathonSecurity
{
    public interface IDataManager
    {
        public Task<int> CreateDataObject(string email);
    }
}