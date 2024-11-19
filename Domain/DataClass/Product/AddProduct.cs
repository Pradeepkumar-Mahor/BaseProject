using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataClass
{
    public class AddProduct
    {
        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        public string? ImgUrl { get; set; }

        [Required]
        public bool ProductIsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}