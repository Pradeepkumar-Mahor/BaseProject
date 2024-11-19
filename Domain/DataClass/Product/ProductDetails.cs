using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataClass
{
    public class ProductDetails
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public bool ProductIsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? ImgUrl { get; set; }
    }
}