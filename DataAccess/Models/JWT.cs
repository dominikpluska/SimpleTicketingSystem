using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    [Keyless]
    public class JWT
    {
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        public string JwtToken { get; set; }
    }
}
