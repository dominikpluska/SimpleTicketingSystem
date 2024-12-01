using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; } 

        [Required]
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
