﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.BLL.DTOs.StudentDTOs.Requests
{
    public class AddStudentRequest
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public string? Gender { get; set; }

        public int? Age { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Grade { get; set; }

        public string? Class { get; set; }

        [Required, MaxLength(50)]
        public string ParentName { get; set; } = null!;

        public string? ParentEmail { get; set; }

        public string? ParentPhone { get; set; }

        public string? ParentJob { get; set; }

        public string? City { get; set; }

        public string? Governorate { get; set; }

        public string? Address { get; set; }

        public string? ParentNid { get; set; }

        public IFormFile? Image { get; set; }
    }
}
