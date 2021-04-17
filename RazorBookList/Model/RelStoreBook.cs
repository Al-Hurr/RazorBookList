using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Model
{
    public class RelStoreBook
    {
        [Key]
        public int Id { get; set; }

        public Book Book { get; set; }

        public Store Store { get; set; }
    }
}
