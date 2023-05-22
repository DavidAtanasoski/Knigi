using Knigi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Knigi.ViewModels
{
    public class KnigaFilterViewModel
    {
        public IList<Kniga> Knigi { get; set; }
        public SelectList Zanrovi { get; set; }
        public string KnigaZanr { get; set; }
        public string SearchNaslov { get; set; }
        public string SearchGodina { get; set; }
        public string SearchAvtor { get; set; }
    }
}
