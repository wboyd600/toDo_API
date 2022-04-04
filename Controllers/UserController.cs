using toDo_API.Models;
using Microsoft.AspNetCore.Mvc;
using toDo_API.Repositories;
namespace toDo_API.Controllers;

[ApiController]
[Route("users")]
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
    public async Task<IActionResult> Create([FromBody]Login data)
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
            var result = new ObjectResult(message) {
                StatusCode = 409
            };
            return result;
        }
        
        if (createdUser != null) {
            var message = new Message();
            message.message = "Success";

            var result = new ObjectResult(message) {
                StatusCode = 201
            };
            var locationString = "/users/" + createdUser.Id.ToString();
            Response.Headers.Add("Location", locationString);
            return result;
        } else {
            return BadRequest();
        }
    }
}