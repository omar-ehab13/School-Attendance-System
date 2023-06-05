using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Constants
{
    public static class TeacherSpechialized
    {
        public static string Arabic = "Arabic"; 
        public static string English = "English"; 
        public static string Math = "Math"; 
        public static string Science = "Science"; 
        public static string SocialStudies = "Social Studies";
        public static string Sports = "Sports";

        public static IList<string> AllSpecialized = new List<string>()
        {
            "Arabic",
            "English",
            "Math",
            "Science",
            "Social Studies",
            "Sports"
        };
    }
}
