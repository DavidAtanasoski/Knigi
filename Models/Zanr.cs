using System.ComponentModel.DataAnnotations;

namespace Knigi.Models
{
    public class Zanr
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Ime { get; set; }

        public ICollection<KnigaZanr>? Knigi { get; set; }
    }
}
