using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            var user=_mapper.Map<User>(addUserDto);
            var userDomainModel=await _userRepository.AddUserAsync(user);
            var response = _mapper.Map<GetUserDto>(userDomainModel);
            return Ok(response);

        }
    }
}
