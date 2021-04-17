using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Model
{
    public class BookCreateModel
    {

        [Required]
        public string Name { get; set; }

        [Display(Name = "Author name")]
        public int AuthorId { get; set; }

        public string ISBN { get; set; }

        [Display(Name = "Cover")]
        public IFormFile Image { get; set; }
    }
}
