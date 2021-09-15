using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineShop.Areas.Admin.Models
{
    public class Item
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}
