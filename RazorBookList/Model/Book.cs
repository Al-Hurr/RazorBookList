using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
