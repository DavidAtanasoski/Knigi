using System.ComponentModel.DataAnnotations;

namespace Knigi.Models
{
    public class Avtor
    { 
        public int Id { get; set; }

        [StringLength(50)]
        public string? Ime { get; set; }

        [StringLength(50)]
        public string? Prezime { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", Ime, Prezime); }
        }

        [StringLength(50)]
        public string? Nacionalnost { get; set; }

        [Display(Name = "Daturm na ragjanje")]
        [DataType(DataType.Date)]
        public DateTime DatumRagjanje { get; set; }

        public ICollection<Kniga>? Knigi { get; set; }
    }
}
