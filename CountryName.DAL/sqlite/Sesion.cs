using System;
using System.IO;
using CountryName;
using CountryName.DAL.Models;
using SQLite;

namespace CountryName.DAL.sqlite
{
    public class Sesion
    {
        string dbPath;
        SQLiteConnection db;
        public Sesion(string dbName)
        {
            try
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);

                db = new SQLiteConnection(dbPath);
                db.CreateTable<Usuario>();
            }
            catch (Exception) { }
        }
        public void Create(Usuario user)
        {
            try
            {
                db.CreateTable<Usuario>();
                db.Insert(user);
            }
            catch (Exception) { }
        }

        public void Update(Usuario user)
        {
            try
            {
                db.Update(user);
            }
            catch (Exception) { }

        }

        public Usuario Current()
        {

            Usuario usr = null;

            try
            {
                usr = (from s in db.Table<Usuario>()
                       orderby s.idUsuario descending
                       select s).FirstOrDefault();
            }
            catch (Exception) { }

            return usr;
        }

        public void EndSession()
        {
            try
            {
                db.DeleteAll<Usuario>();
            }
            catch (Exception) { }
        }
    }
}
