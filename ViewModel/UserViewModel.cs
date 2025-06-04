namespace OrgPortal_CMS.ViewModel
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
