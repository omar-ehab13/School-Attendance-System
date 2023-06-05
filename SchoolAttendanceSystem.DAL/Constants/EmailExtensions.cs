using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Constants
{
    public class EmailExtensions
    {
        public static string SuperAdminExtension = "@superadmin.ibnkhaldun";
        public static string AdminExtension = "@admin.ibnkhaldun";
        public static string ParentExtension = "@parent.ibnkhaldun";
        public static string TeacherExtension = "@teacher.ibnkhaldun";

        public static string? GetEmailExtension(string role)
        {
            if (role == RoleTypes.Parent.ToString())
                return ParentExtension;

            if (role == RoleTypes.Teacher.ToString())
                return TeacherExtension;

            if (role == RoleTypes.Admin.ToString())
                return AdminExtension;

            if (role == RoleTypes.SuperAdmin.ToString())
                return SuperAdminExtension;

            return null;
        }
    }
}
