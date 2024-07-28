using Ecommerce.Core.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUserRepository userRepository;

        public UsersController(ApplicationDbContext context, IUserRepository userRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterFunction([FromBody]RegisterationRequestDTO model) {
            try
            {
                var isUniqe = userRepository.IsUniqueUser(model.Email);
                if(!isUniqe)
                {
                    return BadRequest(new ApiValidationResponse(new List<string> { "email already exsist !!" }, 500));
                }
                var user = await userRepository.Register(model);
                if(user == null)
                {
                    return BadRequest(new ApiValidationResponse(new List<string> { "Error while registeration" }, 500));
                }
                else
                {
                    return Ok(new ApiResponse(200, result: user));
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ApiValidationResponse(new List<string> { ex.Message, "an error accured while processing your request" }));
            }
        }
    }
}
