using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversionPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            var animalType = new AnimalType();
            animalType.AddAnimal(new Animal { Name = "Akita", Color = "Red", Type = Type.Dog });
            animalType.AddAnimal(new Animal { Name = "Arara", Color = "Blue", Type = Type.Bird });
            animalType.AddAnimal(new Animal { Name = "Doberman", Color = "Black", Type = Type.Dog });

            var animalData = new AnimalCount(animalType);
            Console.WriteLine($"The total number of dogs is: { animalData.CountDogs() }");
            Console.WriteLine($"The total number of birds is: { animalData.CountBirds() }");
        }
    }

    public enum Type
    {
        Bird,
        Dog
    }

    public class Animal
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public Type Type { get; set; }
    }

    public interface IAnimalSearch
    {
        IEnumerable<Animal> GetAnimalsByType(Type type);
    }

    public class AnimalType : IAnimalSearch
    {
        private readonly List<Animal> _animals;

        public AnimalType()
        {
            _animals = new List<Animal>();
        }

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }

        public IEnumerable<Animal> GetAnimalsByType(Type type) => _animals.Where(a => a.Type == type);
    }

    public class AnimalCount
    {
        private readonly IAnimalSearch _animalSearch;

        public AnimalCount(IAnimalSearch animalSearch)
        {
            _animalSearch = animalSearch;
        }

        public int CountDogs() => _animalSearch.GetAnimalsByType(Type.Dog).Count();
        
        public int CountBirds() => _animalSearch.GetAnimalsByType(Type.Bird).Count();
    }
}
