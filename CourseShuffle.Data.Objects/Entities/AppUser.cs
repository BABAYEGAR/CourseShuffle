namespace CourseShuffle.Data.Objects.Entities
{
    public class AppUser: TransportObjects
    {
        public  string Firstname { get; set; }
        public  string Lastname { get; set; }
        public  string Othername { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public  string MatricNumber { get; set; }
        public  string MobileNumber { get; set; }
        public  string ProfilePicture { get; set; }
        public  string Level { get; set; }
        public  string Role { get; set; }
        public  Department Department { get; set; }
        public  string DisplayName
            => Firstname + " " + (string.IsNullOrEmpty(Othername) ? "" : Othername) + " " + Lastname;
    }
}