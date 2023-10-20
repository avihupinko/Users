using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using users_backend.Interfaces;
using users_backend.Models;

namespace users_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="phone"></param>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <param name="offeset"></param>
        /// <returns></returns>
        [HttpGet("~/api/Users")]
        public async Task<ActionResult<BasePageModel<UserLogicModel>>> Get([FromQuery] string? userName = null,
                                                      [FromQuery] string? phone = null,
                                                      [FromQuery] string? userId = null,
                                                      [FromQuery] int limit = 10,
                                                      [FromQuery] int offeset = 0)
        {
            return Ok(await _usersService.Get(userName, phone, userId, limit, offeset));
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("~/api/Users/{id}")]
        public async Task<ActionResult<UserLogicModel>> GetById([FromRoute] Guid id)
        {
            UserLogicModel? user = await _usersService.GetById(id);
            if (user != null)
                return Ok(user);
            return BadRequest("User not found");
        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("~/api/Users/{id}")]
        public async Task<ActionResult<UserLogicModel>> Delete([FromRoute] Guid id)
        {
            try
            {
                await _usersService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("~/api/Users")]
        public async Task<ActionResult<UserLogicModel>> Create([FromBody] UserLogicModel model)
        {
            try
            {
                return Ok(await _usersService.Create(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("~/api/Users")]
        public async Task<ActionResult<UserLogicModel>> Update([FromBody] UserLogicModel model)
        {
            try
            {
                return Ok(await _usersService.Update(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
