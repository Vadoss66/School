namespace School.Model
{
    public class AccountModel
    {
        public AccountModel(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public string Login { get; }
        public string Password { get; }
    }
}