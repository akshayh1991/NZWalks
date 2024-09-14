using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(CreateJwtDto createJwtDto)
        {
            var token=_authRepository.CreateJwtToken(createJwtDto.Username);
            var response = new JwtResponseDto
            {
                Token = token,
                Username = createJwtDto.Username,
            };
            return Ok(response);
        }
    }
}
