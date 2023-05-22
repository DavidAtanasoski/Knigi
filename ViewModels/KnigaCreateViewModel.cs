using Knigi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Knigi.ViewModels
{
    public class KnigaCreateViewModel
    {
        public Kniga? Kniga { get; set; }

        public IEnumerable<int>? SelectedZanrovi { get; set; }
        public IEnumerable<SelectListItem>? ZanroviLista { get; set; }
    }
}
