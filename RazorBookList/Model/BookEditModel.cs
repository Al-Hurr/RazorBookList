using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
