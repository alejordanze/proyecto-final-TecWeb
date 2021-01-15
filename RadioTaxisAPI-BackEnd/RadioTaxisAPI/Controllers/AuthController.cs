using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadioTaxisAPI.Models.Security;
using RadioTaxisAPI.Services.Security;

namespace RadioTaxisAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        // /api/auth/userx  
        [HttpPost("user")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRolenAsync([FromBody] CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateRoleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("userRole")]
        public async Task<IActionResult> CreateUserRolenAsync([FromBody] CreateUserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserRoleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

    }
}
