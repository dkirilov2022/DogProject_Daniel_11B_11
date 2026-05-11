using DogsApp.Core.Contracts;
using DogsProject_Daniel_11_11.Data;
using Microsoft.EntityFrameworkCore;
using DogsProject_Daniel_11_11.Data.Domain;

namespace DogsApp.Core.Services;

public class DogService : IDogService
{
    private readonly ApplicationDbContext _context;

    public DogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool Create(string name, int age, int breedId, string? picture, string userId)
    {
        Dog item = new Dog()
        {
            Name = name,
            Age = age,
            Breed = _context.Breeds.Find(breedId),
            Picture = picture,
            OwnerId = userId
        };
        
        _context.Dogs.Add(item);
        return _context.SaveChanges() != 0;
    }

    public Dog GetDogById(int dogId)
    {
        return _context.Dogs
            .Include(d => d.Breed)
            .FirstOrDefault(d => d.Id == dogId);
    }

    public List<Dog> GetDogs()
    {
        List<Dog> dogs = _context.Dogs
            .Include(d => d.Breed)
            .ToList();
        return dogs;
    }
    
    public List<Dog> GetDogs(string searchStringBreed, string searchStringName)
    {
        IQueryable<Dog> query = _context.Dogs
            .Include(d => d.Breed);

        if (!string.IsNullOrEmpty(searchStringBreed) && !string.IsNullOrEmpty(searchStringName))
        {
            query = query.Where(d => d.Breed.Name.Contains(searchStringBreed) && d.Name.Contains(searchStringName));
        }
        else if (!string.IsNullOrEmpty(searchStringBreed))
        {
            query = query.Where(d => d.Breed.Name.Contains(searchStringBreed));
        }
        else if (!string.IsNullOrEmpty(searchStringName))
        {
            query = query.Where(d => d.Name.Contains(searchStringName));
        }

        return query.ToList();
    }

    public bool RemoveById(int dogId)
    {
        var dog = GetDogById(dogId);
        if (dog == default(Dog))
        {
            return false;
        }

        _context.Remove(dog);
        return _context.SaveChanges() != 0;
    }

    public bool UpdateDog(int dogId, string name, int age, int breedId, string? picture)
    {
        var dog = GetDogById(dogId);
        if (dog == default(Dog))
        {
            return false;
        }

        dog.Name = name;
        dog.Age = age;
        dog.BreedId = breedId;
        dog.Picture = picture;
        _context.Update(dog);
        return _context.SaveChanges() != 0;
    }
}