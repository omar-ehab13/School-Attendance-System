using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendanceSystem.DAL.Constants
{
    public enum Subjects
    {
        Arabic,
        English,
        Math,
        Science,
        SocialStudies,
        Skills,
        Sports,
        ReligiousEducation,
        Computer
    }

    public static class InitialCharSubjects
    {
        public static string Arabic = "AR";
        public static string English = "EN";
        public static string Math = "MATH";
        public static string Science = "SCI";
        public static string SocialStudies = "SOC";
        public static string Skills = "SK";
        public static string Sports = "SP";
        public static string ReligiousEducation = "REL";
        public static string Computer = "COMP";

        public static string? GetInitialSubject(string subject)
        {
            switch (subject)
            {
                case nameof(Subjects.Arabic):
                    return Arabic;
                case nameof(Subjects.English):
                    return English;
                case nameof(Subjects.Math):
                    return Math;
                case nameof(Subjects.Science):
                    return Science;
                case nameof(Subjects.SocialStudies):
                    return SocialStudies;
                case nameof(Subjects.Skills):
                    return Skills;
                case nameof(Subjects.Sports):
                    return Sports;
                case nameof(Subjects.ReligiousEducation):
                    return ReligiousEducation;
                case nameof(Subjects.Computer):
                    return Computer;
            }

            return null;
        }
    }
}
