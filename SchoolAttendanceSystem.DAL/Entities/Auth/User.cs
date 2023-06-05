using Microsoft.AspNetCore.Identity;
using SchoolAttendanceSystem.DAL.Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Entities.Auth
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public IList<Parent>? Parents { get; set; }

        public List<Teacher>? Teachers { get; set; }
        //public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
