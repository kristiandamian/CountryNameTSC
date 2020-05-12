using System;
using CountryName.DAL.Logic;

namespace CountryName.DAL
{
    public class Login
    {
        public bool MakeLogin(string user, string password, string dbName)
        {
            bool logged = false;

            var loginlogic = new LoginLogic();
            logged = loginlogic.MakeLogin(user, password);
            if (logged)
            {
                new sqlite.Sesion(dbName).Create(new Models.Usuario
                {
                    User = user,
                    idUsuario = 1,
                });
            }
            return logged;
        }
    }
}
