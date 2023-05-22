namespace Knigi.Models
{
    public class KnigaZanr
    {
        public int Id { get; set; }

        public int KnigaId { get; set; }
        public Kniga? Kniga { get; set; }

        public int ZanrId { get; set; }
        public Zanr? Zanr { get; set; }
    }
}
