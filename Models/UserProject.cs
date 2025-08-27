using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models;

public class UserProject
{
    public int Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
    
    [Range(18, 100)]
    public int Age { get; set; }
    
    [Required]
    public string ProjectName { get; set; } = string.Empty;
    
    public bool IsCompleted { get; set; }
}