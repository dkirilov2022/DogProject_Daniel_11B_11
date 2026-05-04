using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DogsProject_Daniel_11_11.Data.Domain;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
}
