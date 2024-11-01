﻿using Microsoft.EntityFrameworkCore;
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
        public int Id { get; set; }
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

        [Required]
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }

        public bool IsActived { get; set; } = true;
    }
}
