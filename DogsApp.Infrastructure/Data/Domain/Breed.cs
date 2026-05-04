using System.ComponentModel.DataAnnotations;

namespace DogsProject_Daniel_11_11.Data.Domain;

public class Breed
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual IEnumerable<Dog> Dogs { get; set; } = null!;
}