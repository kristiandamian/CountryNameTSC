using System;
namespace CountryName.DAL.Logic
{
    public class LoginLogic : ILogin
    { 
        public LoginLogic()
        {
        }

        public bool MakeLogin(string user, string password)
        {
            if (user == null) user = string.Empty;
            return user.ToLower() == "test@domain.com" && password == "abc123";
        }
    }
}
