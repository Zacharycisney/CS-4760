﻿using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class Grant
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? PI { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Type { get; set; }

        [RegularExpression(@"^(?!0(\.0+)?$)([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$"), Required]
        public decimal Amount { get; set; }

        [Required]
        public string? Status { get; set; }
    }
}
