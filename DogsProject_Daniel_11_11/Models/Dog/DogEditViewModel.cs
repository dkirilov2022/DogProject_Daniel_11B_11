using System.ComponentModel.DataAnnotations;
using DogsProject_Daniel_11_11.Models.Breed;

namespace DogsProject_Daniel_11_11.Models.Dog;

public class DogEditViewModel
{
    public int Id {get; set;}

    [Required]
    [MaxLength(30)]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;
    
    [Range(0, 30, ErrorMessage = "Age must be between 0 and 30")]
    [Display(Name = "Age")]
    public int Age { get; set; }

    [Required]
    [Display(Name = "Breed")]
    public int BreedId { get; set; }

    public List<BreedPairViewModel> Breeds { get; set; } = new List<BreedPairViewModel>();
    
    [Display(Name = "Dog Picture")]
    public string? Picture { get; set; }
}