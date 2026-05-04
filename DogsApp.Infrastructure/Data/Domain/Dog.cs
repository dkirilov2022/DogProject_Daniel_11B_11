using System.ComponentModel.DataAnnotations;

namespace DogsProject_Daniel_11_11.Data.Domain;

public class Dog
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;
    
    [Range(0, 30)]
    public int Age { get; set; }

    [Required]
    public int BreedId { get; set; }

    public virtual Breed Breed { get; set; } = null!;
    
    public string? Picture { get; set; }
}