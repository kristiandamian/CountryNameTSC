using System;
namespace CountryName.DAL.Logic
{
    internal interface ILogin
    {
        bool MakeLogin(string user, string password);
    }
}
