using Application.Dtos.Request.Account;
using Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Application.Dtos.Responses.ServiceRersponse;

namespace WebApi.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepo user) : ControllerBase
    {
        [HttpPost("login")]

        public async Task<ActionResult<LoginResponse>> Login(LoginModelDto model)
        {
            var rersult = await user.LoginAsync(model);
            return Ok(rersult);
        }

        [HttpPost("register")]

        public async Task<ActionResult<RegisterResponse>> Register(RegisterModelDto model)
        {
            var rersult = await user.RegisterAsync(model);
            return Ok(rersult);
        }
    }
}
