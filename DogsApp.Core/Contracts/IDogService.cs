using DogsProject_Daniel_11_11.Data.Domain;

namespace DogsApp.Core.Contracts;

public interface IDogService
{
    bool Create(string name, int age, int breedId, string picture);
    bool UpdateDog(int dogId, string name, int age, int breedId, string? picture);
    List<Dog> GetDogs();
    Dog GetDogById(int dogId);
    bool RemoveById(int dogId);
    List<Dog> GetDogs(string searchStringBreed, string searchStringName);
}