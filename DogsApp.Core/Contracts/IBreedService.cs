using DogsProject_Daniel_11_11.Data.Domain;

namespace DogsApp.Core.Contracts;

public interface IBreedService
{
    List<Breed> GetBreeds();
    Breed GetBreedById(int breedId);
    List<Dog> GetDogsByBreed(int BreedId);
}