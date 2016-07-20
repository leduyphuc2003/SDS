using System.Collections.Generic;
using ItemMaster.Web.Models;

namespace ItemMaster.Web.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<ZppStock> Items { get; set; }
    }
}