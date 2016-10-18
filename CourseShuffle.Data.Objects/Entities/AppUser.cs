namespace CourseShuffle.Data.Objects.Entities
{
    public class AppUser
    {
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Othername { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string MatricNumber { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string Level { get; set; }
        public virtual string Role { get; set; }
        public virtual Department Department { get; set; }
        public virtual string DisplayName
            => Firstname + " " + (string.IsNullOrEmpty(Othername) ? "" : Othername) + " " + Lastname;
    }
}