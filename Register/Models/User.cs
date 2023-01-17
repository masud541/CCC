using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Register.Models
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; } = null!;
    }
}
