﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Position
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(3, 30)]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}