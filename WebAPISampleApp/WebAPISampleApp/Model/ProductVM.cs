using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Data;

namespace WebAPISampleApp.Model
{
    public class ProductVM
    {
        [Key]
        public Guid ProdId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public byte SalePercent { get; set; }
        public string category { get; set; }
    }
}
