using System;

namespace OnlineShop.Models
{
    public class ErrorViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
