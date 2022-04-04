using toDo_API.Repositories;

namespace toDo_API.Models
{
    public class User: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public String Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
    public class CreateUser {
        public Data data { get; set; }
        public class Data {
            public String Username { get; set; }
            public string Password { get; set; }
        }
    }

    public class UserData
    {
        public Guid Id { get; set; }
        public String Username { get; set; }
    }

    public class Message
    {
        public String message { get; set; }
    }
}