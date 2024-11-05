using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TicketId")]
        public int TicketId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        [StringLength(600)]
        public string CommentField { get; set; }

    }
}
