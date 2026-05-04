using System.ComponentModel.DataAnnotations;

namespace DogsProject_Daniel_11_11.Models.Breed;

public class BreedPairViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Breed")]
    public string Name { get; set; }
}