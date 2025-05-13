using System.ComponentModel.DataAnnotations;

namespace Prog7311.Poe.Models
{
    public enum UserRole
    {
        Farmer, 
        Employee 
    }

    public class UserModel
    {
        public int Id { get; set; }
        public required string FarmerName { get; set; } = string.Empty;
        public required string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public string FarmName { get; set; } = string.Empty;
        public string FarmAddress { get; set; } = string.Empty;
        public string FarmLocation { get; set; } = string.Empty;
        public string FarmSize { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Farmer;
    }
}