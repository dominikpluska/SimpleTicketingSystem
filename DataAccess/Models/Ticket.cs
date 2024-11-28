using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string TicketType { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId{ get; set; }
        [NotMapped]
        public Category Category { get; set; }
        [Required]
        public string Severity { get; set; }
        public string AssigmentGroup { get; set; } 
        [Required]
        public DateTime CreationDate { get; set; } 
        [Required]
        public DateTime LastModifiedDate { get; set; } 

    }
}
