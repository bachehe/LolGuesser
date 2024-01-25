using API.DTO.User;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var login = await _service.Login(loginDto);

            if (login == null) return BadRequest();

            return login.Email.IsNullOrEmpty() ? NoContent() : Ok(login);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var register = await _service.Register(registerDto);

            if (register == null) return BadRequest();

            return register.Email.IsNullOrEmpty() ? NoContent(): Ok(register);
        }
    }
}
