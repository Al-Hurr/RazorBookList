using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Author name")]
        public Author Author { get; set; }


        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        public byte[] Cover { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
