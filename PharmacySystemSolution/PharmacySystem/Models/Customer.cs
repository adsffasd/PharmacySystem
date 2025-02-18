﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Bill>? Bills { get; set; }
    }
}
