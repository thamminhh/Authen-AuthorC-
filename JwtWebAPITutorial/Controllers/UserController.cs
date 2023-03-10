
using JwtWebAPITutorial.Entities;
using JwtWebAPITutorial.Interface;
using JwtWebAPITutorial.Entities_SubModel.User;
using Microsoft.AspNetCore.Mvc;
using JwtWebAPITutorial.Endpoints;
using JwtWebAPITutorial.Entities_SubModel.User.SubModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using JwtWebAPITutorial.Constant;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private static readonly IList<User> _users = new List<User>();


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet, Authorize(Roles = UserRoleConstant.Admin + "," + UserRoleConstant.Expertise)]
        [Route(UserEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers([FromQuery] UserFilter filter, int page = 1, int pageSize = 10)
        {
            var listUser = _userRepository.GetUsers(page, pageSize, filter);
            int toalCount = _userRepository.GetNumberOfUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { users = listUser, total = toalCount });
        }

        [HttpGet, Authorize(Roles = UserRoleConstant.Admin)]
        [Route(UserEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUserById(int id)
        {
            if (!_userRepository.UserExit(id))
                return NotFound();
            var user = _userRepository.GetUserById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpPost]
        [Route(UserEndpoints.Create)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] CreateUserModel userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.CreateUser(userCreate, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully Added");
        }
        [HttpPut]
        [Route(UserEndpoints.UpdateRole)]
        public async Task<IActionResult> UpdateUserRole([FromRoute] int id, [FromBody] UpdateUserRoleModel model)
        {
            if (model == null)
                return BadRequest(ModelState);

            if (!_userRepository.UserExit(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userRepository.UpdateUserRoleAsync(id, model))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}



