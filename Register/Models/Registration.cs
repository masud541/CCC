using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Register.Models
{
    [Table("Registration")]
    public partial class Registration
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(100)]
        [DisplayName("Comany Name")]
        public string? ComanyName { get; set; }
        [StringLength(100)]
        public string? City { get; set; }
        [StringLength(100)]
        public string? State { get; set; }
        [StringLength(20)]
        public string? PhoneNo { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Invalid Email address")]
        public string Email { get; set; } = null!;
        [StringLength(250)]
        [DisplayName("Company Address")]
        [Required]
        public string CompanyAddress { get; set; } = null!;
        [StringLength(250)]
        public string Country { get; set; } = null!;
        [StringLength(80)]
        public string? Zipcode { get; set; }
    }
}
