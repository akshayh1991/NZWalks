using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region=await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) 
            {
                return null;
            }
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var regions = await _context.Regions.ToListAsync();
            return regions;
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
           var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regionRes=await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionRes == null) 
            {
                return null;
            }
            
            regionRes.Code = region.Code;
            regionRes.Name = region.Name;
            regionRes.RegionImageUrl = region.RegionImageUrl;
            await _context.SaveChangesAsync();
            return regionRes;
        }
    }
}
