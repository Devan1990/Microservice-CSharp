namespace CAT.BFF.Models
{
    public class UsersVm
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleVm2 Role { get; set; }
        
    }
}
