using toDo_API.Repositories;

namespace toDo_API.Models
{
    partial class LoginResponse {
        public LoginResponse(string message, string token, Guid id) 
        {
            this.message = message;
            this.data = new LoginResponse.Data();
            this.data.token = token;
            this.data.id = id;
        }
        public string message {get; set;}
        public Data data { get; set; }
    }
}