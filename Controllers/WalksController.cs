using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        private readonly ILogger _logger;

        public WalksController(IMapper mapper,IWalkRepository walkRepository,ILogger<WalksController> logger)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody]CreateWalkDto walkDto)
        {
            //map dto to domain model
            var walk=_mapper.Map<Walk>(walkDto);
            await _walkRepository.CreateAsync(walk);
            return Ok(_mapper.Map<GetWalkDto>(walk));

        }

        [HttpGet]
        [Route("FSP")]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? nameFilter, [FromQuery] bool isAscending = true,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageRecords = 1000)
        {
            var res1 = await _walkRepository.GetAllWalksAsync(nameFilter, isAscending, pageNumber, pageRecords);
            var res = _mapper.Map<List<GetWalkDto>>(await _walkRepository.GetAllWalksAsync(nameFilter, isAscending, pageNumber, pageRecords));
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            _logger.LogInformation("Akshay Huded");
            var res1 = await _walkRepository.GetAllWalksAsync();
            var res = _mapper.Map<List<GetWalkDto>>(await _walkRepository.GetAllWalksAsync());
            _logger.LogInformation($"Get All the data {JsonSerializer.Serialize(res)}");
            return Ok(res);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetWalkById(Guid guid)
        {
            var walk=await _walkRepository.GetWalkByIdAsync(guid);
            if (walk==null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<GetWalkDto>(walk));
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateWalk(Guid guid,[FromBody]UpdateWalkDto walk)
        {
            var walkDomain=_mapper.Map<Walk>(walk);
            var res=await _walkRepository.UpdateWalkAsync(guid, walkDomain);
            if (res==null)
            {  return BadRequest(); }
            return Ok(_mapper.Map<GetWalkDto>(res));
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteWalk(Guid guid)
        {
            var walk=await _walkRepository.DeleteWalkAsync(guid);
            if(walk==null)
            { return BadRequest(); }
            return Ok();
        }
    }
}
