﻿using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Entities
{
    public class Attendee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(150, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public int CourseId { get; set; }
    }
}
