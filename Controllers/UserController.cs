using toDo_API.Models;
using Microsoft.AspNetCore.Mvc;
using toDo_API.Repositories;
using System.Text;
namespace toDo_API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UserController(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<IEnumerable<UserData>>> GetAll() {
        var results = await _userRepository.All(user => true);
        List<UserData> userDatas = new List<UserData>();
        foreach (User user in results) 
        {
            var userData = new UserData();
            userData.Username = user.Username;
            userData.Id = user.Id;
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
            var message = new Message();
            message.message = e.Message;
            return Conflict(message);
        }
        
        if (createdUser != null) {
            var message = new Message();
            message.message = "Success";
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
        User? loggedIn;
        try {
            loggedIn = await _userRepository.Login(user);
        } catch (InvalidOperationException e) 
        {
            var message = new Message();
            message.message = e.Message;
            return Conflict(message);
        }

        if (loggedIn != null) {
            var locationString = "/users/" + loggedIn.Id.ToString();
            return NoContent();
        }

        return BadRequest();
    }
}