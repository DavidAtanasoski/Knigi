using Knigi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Knigi.ViewModels
{
    public class KnigaEditViewModel
    {
        public Kniga? Kniga { get; set; }
        public IEnumerable<int>? SelektiraniZanrovi { get; set; }
        public IEnumerable<SelectListItem>? ZanrLista { get; set; }
    }
}
