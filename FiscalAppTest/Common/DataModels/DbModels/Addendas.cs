using DataModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DataModels.DbModels
{
    public class Addendas : IModelEntity
    {

        [Key]
        public Guid? IdAddenda { get; set; }
        public string? NombreAddenda { get; set; }
        public string? XML { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Usuario { get; set; }
        public bool? Estado { get; set; }
    }
}