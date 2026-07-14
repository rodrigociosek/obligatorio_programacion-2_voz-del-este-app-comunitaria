
using SQLite;
using System.Diagnostics;

namespace VozDelEsteMaui3.Data
{
   public class SQLiteDbContext
   {
      private readonly SQLiteAsyncConnection _conexion;
      public SQLiteAsyncConnection Conexion => _conexion;

      public SQLiteDbContext()
      {
            //string rutaBD = Path.Combine(FileSystem.AppDataDirectory, "vozdeleste.db3");

            //if (File.Exists(rutaBD))
            //{
            //    File.Delete(rutaBD);
            //}

            var dbRuta = Path.Combine(FileSystem.AppDataDirectory, "vozdeleste.db3");
         _conexion = new SQLiteAsyncConnection(dbRuta);
      }
   }
}
