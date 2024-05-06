using Microsoft.AspNetCore.Http;
using System.Diagnostics.Metrics;

namespace SchoolPortal.Utility
{
    public class StaticDetails
    {
        //All roles are declared in DB therefore, adding here when db is created will not add more. Check DBInitializer
        public const string Role_Student = "Student";
        public const string Role_Teacher = "Teacher";
        public const string Role_Admin = "Admin";

        //Strings are not in DBInitializer. Can be extended by adding here.
        public const string BranchOfService_PAF = "PAF";
        public const string BranchOfService_Test = "TEST";
        
        public const string Rank_Airman = "Airman (AM)";
        public const string Rank_Airman2C = "Airman 2nd Class (A2C)";
        public const string Rank_Airman1C = "Airman 1st Class (A1C)";
        public const string Rank_Sergeant = "Sergeant (SGT)";
        public const string Rank_StaffSergeant = "Staff Sergeant (SSGT)";
        public const string Rank_TechnicalSergeant = "Technical Sergeant (TSGT)";
        public const string Rank_MasterSergeant = "Master Sergeant (MSGT)";
        public const string Rank_SeniorMasterSergeant = "Senior Master Sergeant (SMSGT)";
        public const string Rank_ChiefMasterSergeant = "Chief Master Sergeant (CMSGT)";
        public const string Rank_2ndLieutenant = "2nd Lieutenant (2LT)";
        public const string Rank_1stLieutenant = "1st Lieutenant (1LT)";
        public const string Rank_Captain = "Captain (CPT)";
        public const string Rank_Major = "Major (MAJ)";
        public const string Rank_LieutenantColonel = "Lieutenant Colonel (LTC)";
        public const string Rank_Colonel = "Colonel (Colonel)";
        public const string Rank_Test = "Test";
    }
}
