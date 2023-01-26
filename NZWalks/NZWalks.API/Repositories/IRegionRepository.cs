using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllAsync();
       Task<Region> GetAsyncRegion(Guid id);
       Task<Region> AddAsync(Region region);
       Task<Region> DeleteAsyncRegion(Guid id);
        Task<Region> UpdateAsyncRegion(Guid id, Region region);
    }
}
