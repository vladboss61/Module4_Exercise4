namespace ConsoleEFCore.DbModels
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string About { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
