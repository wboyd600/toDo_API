namespace toDo_API.Models
{
    partial class LoginResponse {
        public class Data {
            public string token { get; set; } = string.Empty;
            public Guid id { get; set; }
        }
    }
}