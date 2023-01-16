namespace dani.Models.Request
{
    public class UserRegisterRequest
    {
        public string Email { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
