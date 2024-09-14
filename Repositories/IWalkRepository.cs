using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? nameFilter, bool isAscending,int pageNumber,int pageRecords);
        Task<List<Walk>> GetAllWalksAsync();
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk?>UpdateWalkAsync(Guid id, Walk walk);
        Task<bool?> DeleteWalkAsync(Guid guid);
    }
}
