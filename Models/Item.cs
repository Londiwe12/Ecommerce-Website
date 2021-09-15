using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
   
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public Products Products { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
