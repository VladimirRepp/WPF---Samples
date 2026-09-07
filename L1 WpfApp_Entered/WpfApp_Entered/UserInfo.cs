using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Entered
{
    public static class UserInfo
    {
        public static string Name;
        public static string Password;
        public static string City;
        public static bool IsAgreement;
        public static EAgeState Age;
        public static EGenderState Gender;
    }

    public enum EAgeState
    {
        Before16,
        Between16_22,
        After22
    }

    public enum EGenderState
    {
        Male,
        Female
    }
}
