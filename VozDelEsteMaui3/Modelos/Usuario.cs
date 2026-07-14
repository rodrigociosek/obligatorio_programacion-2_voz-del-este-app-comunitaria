using SQLite;
using System.ComponentModel.DataAnnotations;

namespace VozDelEsteMaui3.Modelos
{
   [SQLite.Table("Usuarios")]
   public class Usuario
   {
      [PrimaryKey, AutoIncrement]
      public int Id { get; set; }

      [SQLite.MaxLength(50),Unique, NotNull]
      public string Alias { get; set; }

      [SQLite.MaxLength(100), NotNull]
      public string Clave { get; set; }

      [SQLite.MaxLength(250), NotNull]
      public string NombreCompleto { get; set; }

      [SQLite.MaxLength(500)]
      public string Direccion { get; set; }

      [NotNull, Phone]
      public string Telefono { get; set; }

      [NotNull, EmailAddress, Unique]
      public string Email { get; set; }

      public string FotoUrl { get; set; } = "";

      public bool EsAdmin { get; set; }
   }
}
