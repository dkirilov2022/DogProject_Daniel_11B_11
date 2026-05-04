using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogsProject_Daniel_11_11.Models.Dog;

public class DogAllViewModel
{
    public int Id{ get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; } = null!;
    
    [Display(Name = "Age")]
    public int Age { get; set; }
    
    [Display(Name = "Breed")]
    public string BreedName { get; set; } = null!;
    
    [Display(Name = "Dog Picture")]
    public string? DogPicture { get; set; }
}