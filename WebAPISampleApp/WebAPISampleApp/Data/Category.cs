using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
