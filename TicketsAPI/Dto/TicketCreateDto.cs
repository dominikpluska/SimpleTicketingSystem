using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketsAPI.Dto
{
    public class TicketCreateDto
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
        public int CategoryId { get; set; }
        [Required]
        public string Severity { get; set; }
        public string AssigmentGroup { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
    }
}
