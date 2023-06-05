using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Constants
{
    public static class RolesId
    {
        public static string SuperAdminRoleId = "c7dedf49-21a9-4f26-8c00-eadd86eb812f";
        public static string AdminRoleId = "98f4df00-a720-4794-bacb-921996ae6d22";
        public static string ParentRoleId = "7de9e0d6-7698-454c-8093-f5cc111bc251";
        public static string TeacherRoleId = "c04651d9-82f3-4096-951c-cfa33d654382";

        public static string SuperAdminConcurrencyStamp = "929f3c01-f56e-45fb-80e5-59460195b908";
        public static string AdminConcurrencyStamp = "294d9109-735e-4481-9754-52c8b4c87ed8";
        public static string ParentConcurrencyStamp = "45259077-663d-4409-ab4b-aab2da1d5d27";
        public static string TeacherConcurrencyStamp = "0fa3dc95-f069-4e0b-9d6e-aa7370bbcb73";
    }
}
