using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext context,IRegionRepository regionRepository,IMapper mapper)
        {
            _context = context;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public  async Task<IActionResult> GetAllRegions()
        {
            //var regions=await _context.Regions.ToListAsync();
            var regions = await _regionRepository.GetAllRegionsAsync();
            //Map Domain models into dto
            //var regionDto = new List<RegionDto>();
            //foreach (var region in regions) 
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            var regionDto=_mapper.Map<List<RegionDto>>(regions);
            return Ok(regionDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            //var region= _context.Regions.Find(id);
            //var region = await _context.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            var region = await _regionRepository.GetRegionByIdAsync(id);
            if (region == null) 
            {
                return NotFound();
            }
            //Map 
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto=_mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody]AddRegionDto regionDto)
        {
            //var addRegion = new Region
            //{
            //    Name = regionDto.Name,
            //    Code = regionDto.Code,
            //    RegionImageUrl = regionDto.RegionImageUrl
            //};
            var addRegion=_mapper.Map<Region>(regionDto);
            //await _context.Regions.AddAsync(addRegion);
            //await _context.SaveChangesAsync();
            var res=await _regionRepository.AddRegionAsync(addRegion);
            // map domain model to dto
            //var region = new RegionDto
            //{
            //    Id = res.Id,
            //    Name = res.Name,
            //    Code = res.Code,
            //    RegionImageUrl = res.RegionImageUrl
            //};
            var region=_mapper.Map<RegionDto>(res);
            return CreatedAtAction(nameof(GetRegionById), new { id = addRegion.Id }, region);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(Guid id,[FromBody]UpdateRegionDto regionDto)

        {
            //var region=await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //var regionDomainModel = new Region
            //{
            //    Name = regionDto.Name,
            //    Code = regionDto.Code,
            //    RegionImageUrl = regionDto.RegionImageUrl
            //};
            var regionDomainModel = _mapper.Map<Region>(regionDto);
            var region = await _regionRepository.UpdateRegionAsync(id, regionDomainModel);
            if (region == null) { return NotFound(); }
            //var updateRegion = new Region
            //{
            //    Name = regionDto.Name,
            //    Code = regionDto.Code,
            //    RegionImageUrl = regionDto.RegionImageUrl
            //};

            //region.Name = regionDto.Name;
            //region.Code = regionDto.Code;
            //region.RegionImageUrl = regionDto.RegionImageUrl;
            ////_context.Regions.Update(updateRegion);
            //await _context.SaveChangesAsync();

            //var rdto = new RegionDto
            //{
            //    Id=region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var rdto = _mapper.Map<RegionDto>(region);
            return Ok(rdto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(Guid id) 
        {
            //var region=await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var region = await _regionRepository.DeleteRegionAsync(id);
            if (region == null) { return NotFound(); };
            //_context.Regions.Remove(region);
            //await _context.SaveChangesAsync();
            //var r = new RegionDto
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var r = _mapper.Map<RegionDto>(region);
            return Ok(r);
        }
    }
}
