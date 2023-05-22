using System.ComponentModel.DataAnnotations;

namespace Knigi.Models
{
    public class Kniga
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? Naslov { get; set; }

        [Display(Name = "Avtor")]
        public int AvtorId { get; set; }
        public Avtor? Avtor { get; set; }

        public int? Godina { get; set; }

        public string? Opis { get; set; }

        [Display(Name = "Naslovna")]
        public string? SlikaUrl { get; set; }

        public ICollection<KnigaZanr>? Zanrovi { get; set; }
    }
}
