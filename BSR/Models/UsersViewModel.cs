namespace BSR.Models;

public class UsersViewModel
{
    public List<UserViewModel> Users { get; set; }
}

public class UserViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}