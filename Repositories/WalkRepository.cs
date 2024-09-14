using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;

        public WalkRepository(NZWalksDbContext context)
        {
            _context = context;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<bool?> DeleteWalkAsync(Guid guid)
        {
            var walk=await _context.Walks.FirstOrDefaultAsync(x=>x.Id == guid);
            if (walk == null) { return null;}
            _context.Walks.Remove(walk);
            return true;
        }

        public async Task<List<Walk>> GetAllWalksAsync(string? nameFilter, bool isAscending,int pageNumber,int pageRecords)
        {
            var walk =  _context.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                walk = walk.Where(x => x.Name.Contains(nameFilter));
            }
                walk=(isAscending) ? walk.OrderBy(x => x.Name) : walk.OrderByDescending(x => x.Name);

            //pagination
            var skipResults = (pageNumber - 1) * pageRecords;
            return await walk.Skip(skipResults).Take(pageRecords).ToListAsync();
            //return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk= await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id == id);
            if (walk == null) return null;
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkRes=  await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkRes == null) return null;
            walkRes.Name=walk.Name;
            walkRes.Description=walk.Description;
            walk.LengthInKm=walkRes.LengthInKm;
            walk.WalkImageUrl=walkRes.WalkImageUrl;
            walk.DifficultyId=walkRes.DifficultyId;
            walk.RegionId=walkRes.RegionId;
            _context.Walks.Update(walkRes);
            await _context.SaveChangesAsync();
            return walk;
        }
    }
}
