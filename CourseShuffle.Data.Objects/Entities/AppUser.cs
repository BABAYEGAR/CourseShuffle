using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseShuffle.Data.Objects.Entities
{
    public class AppUser: TransportObjects
    {
        public long AppUserId { get; set; }
       
        public  string Firstname { get; set; }
        public  string Lastname { get; set; }
        public  string Othername { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        [DisplayName("Mobile Number")]
        public  string MobileNumber { get; set; }
        public  string ProfilePicture { get; set; }
        public  string Role { get; set; }
        [DisplayName("Department")]
        public long DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public  Department Department { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
        public  string DisplayName
            => Firstname + " " + (string.IsNullOrEmpty(Othername) ? "" : Othername) + " " + Lastname;
    }
}