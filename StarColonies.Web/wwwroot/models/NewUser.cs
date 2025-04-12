namespace StarColonies.Web.wwwroot.models;

public class NewUser
{
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
    
    public string SettlerName { get; set; } = "";

    public string BirthdayEntry { get; set; } = "";
    
    public string Profession { get; set; } = "";
    
    public string ProfilePicture { get; set; } = "";

    public string Statistics { get; set; } = "";
    
    public DateOnly Birthday { get; set; } = new DateOnly();
    
    public int StrenghStat { get; set; } = 0;
    
    public int StaminaStat { get; set; } = 0;
}