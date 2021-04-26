using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Model
{
    public class BookEditModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int AuthorId { get; set; }

        public string ISBN { get; set; }

        [Display(Name = "Cover")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
