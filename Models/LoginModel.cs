using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class LoginModel
{
   
    public required string Username { get; set; }

    
    public required string Password { get; set; }
}