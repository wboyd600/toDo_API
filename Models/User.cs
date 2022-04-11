using toDo_API.Repositories;

namespace toDo_API.Models
{
    public class User: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
    }

    public class CreateUser {
        public Data data { get; set; } = new Data();
        public class Data {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }

    public class UserData
    {
        public UserData(
            Guid id,
            string username
        ) 
        {
            this.Id = id;
            this.Username = username;
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    public class Message
    {
        public Message(String message) 
        {
            this.message = message;
        }
        public String message { get; set; }
    }
}