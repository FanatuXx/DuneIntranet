using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFF.Api.Dtos;
using PFF.Api.Infrastructure;
using PFF.Domain.Model.Entities;
using PFF.Domain.Repositories;
using PFF.Shared.Dtos;

namespace PFF.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IList<User> users, ITokenService tokenService) : ControllerBase
    {
        private readonly IList<User> _users = users;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            User? user = _users.SingleOrDefault(u => u.Username == dto.Username);

            if (user is null)
                return NotFound();

            string token = _tokenService.GenerateToken(user);
            user.RefreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new UserDto(user.Id, user.Username, user.Role, token, user.RefreshToken));
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenDto dto)
        {
            int id = _tokenService.GetUserIdFromToken(dto.Token);

            User? user = _users.SingleOrDefault(u => u.Id == id);

            if (user is null)
                return BadRequest();

            if (user.RefreshToken != dto.RefreshToken)
                return BadRequest();

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            string token = _tokenService.GenerateToken(user);

            return Ok(new TokenPairDto(token, user.RefreshToken));
        }
    }
}
