using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Model
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Display(Name = "Working time")]
        [DataType(DataType.Time)]
        public DateTime WorkingTimeFrom { get; set; }
        [DataType(DataType.Time)]
        public DateTime WorkingTimeTo { get; set; }
    }
}
