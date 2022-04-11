using toDo_API.Models;
using Microsoft.AspNetCore.Mvc;
using toDo_API.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
namespace toDo_API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly string secretKey;
    public UserController(
        IUserRepository userRepository,
        IUserService userService,
        ApplicationConfig applicationConfig
    )
    {
        _userRepository = userRepository;
        _userService = userService;
        secretKey = applicationConfig.Key;
    }

    [HttpGet]
    [Route("users")]
    // [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserData>>> GetAll() {
        var results = await _userRepository.All(user => true);
        List<UserData> userDatas = new List<UserData>();
        foreach (User user in results) 
        {
            var userData = new UserData(user.Id, user.Username);
            userDatas.Add(userData);
        }
        return userDatas;
    }
    
    [HttpPost]
    [Route("users")]
    public async Task<IActionResult> Create([FromBody]CreateUser data)
    {
        
        var user = new User();
        user.Username = data.data.Username;
        user.Password = data.data.Password;
        User createdUser;
        try {
            createdUser = await _userRepository.Create(user);
        } catch (InvalidOperationException e) {
            var message = new Message(e.Message);
            return Conflict(message);
        }
        
        if (createdUser != null) {
            var message = new Message("Success");
            var locationString = "/users/" + createdUser.Id.ToString();
            IActionResult response = Created(locationString, message);
            return response;
        } else {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]CreateUser data)
    {
        var user = new User();
        user.Username = data.data.Username;
        user.Password = data.data.Password;
        var users = await _userRepository.All(i => i.Username == user.Username);
        var currentUser = users.FirstOrDefault();

        // Response for invalid username or passwords
        var message = new Message("Invalid username or password");

        if (currentUser == null) {
            return Unauthorized(message);
        }

        var salt = currentUser.Salt;
        var validPassword = Helpers.Crypto.VerifyPassword(user.Password, currentUser.Password, salt);

        if (validPassword) {
            var jwt = Helpers.Crypto.CreateToken(currentUser, secretKey);
            var response = new LoginResponse();
            response.message = "success";
            var dataObject = new LoginResponse.Data();
            dataObject.token = jwt;
            dataObject.id = currentUser.Id;
            response.data = dataObject;
            return Ok(response);
        } else {
            return Unauthorized(message);
        }
    }

    [HttpDelete]
    [Route("users/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id) {
        var currentUser = new Guid(_userService.GetMyID());
        if (currentUser != id) {
            return Forbid();
        }

        var user = await _userRepository.Delete(currentUser);
        if (user != null) {
            return NoContent();
        }

        return NotFound();
    }
}