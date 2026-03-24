using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerSystem.Mammals;
using WildlifeTrackerSystem.Birds;
using WildlifeTrackerSystem.Fish;
using WildlifeTrackerV3.List;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using WildlifeTrackerV3.FoodItems;
using WildlifeTrackerV2.Animals;
using Newtonsoft.Json.Converters;

namespace WildlifeTrackerV4.Utilities
{
    public static class FileHandler 
    {
        /// <summary>
        /// Save the data from the animal list including the category data to a text file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="animals"></param>
        /// <exception cref="Exception"></exception>
        public static void SaveToTextFile(string filePath, ListManager<Animal> animalList)
        {
            List<Animal> animals = animalList.List;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < animals.Count; i++)
                    {
                        Animal animal = animals[i];
                        string text = $"{animals[i].Id};{animals[i].Name};{animals[i].Age};{animals[i].Gender};";

                        string category = animal.Category.ToString();
                        string species = string.Empty;
                        if(animal is Cat cat)
                        {
                            species = cat.Species.ToString();
                            text += $"{category};{species};{cat.Habitat};{cat.TeethCount}";
                        }
                        else if(animal is Otter otter)
                        {
                            species = otter.Species.ToString();
                            text += $"{category};{species};{otter.Habitat};{otter.TeethCount}";
                        }
                        else if(animal is Tiger tiger)
                        {
                            species = tiger.Species.ToString();
                            text += $"{category};{species};{tiger.Habitat};{tiger.TeethCount}";
                        }
                        else if (animal is Falcon falcon)
                        {
                            species = falcon.Species.ToString();
                            text += $"{category};{species};{falcon.WingSpan};{falcon.FlightSpeed}";
                        }
                        else if (animal is Stork stork)
                        {
                            species = stork.Species.ToString();
                            text += $"{category};{species};{stork.WingSpan};{stork.FlightSpeed}";
                        }
                        else if (animal is Goldfish goldfish)
                        {
                            species = goldfish.Species.ToString();
                            text += $"{category};{species};{goldfish.WaterType};{goldfish.FinCount}";
                        }
                        else if (animal is OrangeClownfish orangeClownfish)
                        {
                            species = orangeClownfish.Species.ToString();
                            text += $"{category};{species};{orangeClownfish.WaterType};{orangeClownfish.FinCount}";
                        }
                        else if (animal is WhiteShark whiteShark)
                        {
                            species = whiteShark.Species.ToString();
                            text += $"{category};{species};{whiteShark.WaterType};{whiteShark.FinCount}";
                        }

                        writer.WriteLine(text);
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                throw new Exception("Access to the file was denied.", ex);
            }
            catch (IOException ex)
            {
                throw new Exception("An IO error occured hile saving to a text file", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred during file handling.", ex);
            }
        }

        /// <summary>
        /// Read animal data from a text file and set it in the animal list. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static AnimalManager ReadFromTextFile(string filePath)
        {
            AnimalManager manager = new AnimalManager();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i<lines.Length; i++)
                {
                    string line = lines[i];
                    string[] values = line.Split(';');

                    Animal animal = null;

                    Category category = (Category)Enum.Parse(typeof(Category), values[4]);
                    
                    switch (category)
                    {
                        case Category.Mammal:
                            MammalSpecies mammalSpecies = (MammalSpecies)Enum.Parse(typeof(MammalSpecies), values[5]);
                            string habitat = values[6];
                            int teethCount = int.Parse(values[7]);
                            if (mammalSpecies == MammalSpecies.Cat)
                            {
                                animal = new Cat(habitat, teethCount);
                            }
                            else if (mammalSpecies == MammalSpecies.Otter)
                            {
                                animal = new Otter(habitat, teethCount);
                            }
                            else
                            {
                                animal = new Tiger(habitat, teethCount);
                            }
                            break;
                        case Category.Bird:
                            BirdSpecies birdSpecies = (BirdSpecies)Enum.Parse(typeof(BirdSpecies), values[5]);
                            double wingSpan = double.Parse(values[6]);
                            double flightSpeed = double.Parse(values[7]);
                            if(birdSpecies == BirdSpecies.Falcon)
                            {
                                animal = new Falcon(wingSpan, flightSpeed);
                            }
                            else
                            {
                                animal = new Stork(wingSpan, flightSpeed);
                            }
                            break;
                        case Category.Fish:
                            FishSpecies fishSpecies = (FishSpecies)Enum.Parse(typeof(FishSpecies), values[5]);
                            string waterType = values[6];
                            int finCount = int.Parse(values[7]);
                            if(fishSpecies == FishSpecies.Goldfish)
                            {
                                animal = new Goldfish(waterType, finCount);
                            }
                            else if(fishSpecies == FishSpecies.Orange_clownfish)
                            {
                                animal = new OrangeClownfish(waterType, finCount);
                            }
                            else
                            {
                                animal = new WhiteShark(waterType, finCount);
                            }
                            break;

                    }

                    if (animal != null)
                    {
                        animal.Id = values[0];
                        animal.Name = values[1];
                        animal.Age = int.Parse(values[2]);
                        animal.Gender = (Gender)Enum.Parse(typeof(Gender), values[3]);
                        animal.Category = category;


                        manager.Add(animal);
                    }

                }

                return manager;
            }
            catch (FileNotFoundException ex)
            {
                throw new IOException("The text file was not found.", ex);
            }
           
           

        }

        /// <summary>
        /// Save animal data in JSON format.(including data from category and species)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="animals"></param>
        /// <exception cref="Exception"></exception>
        public static void SaveToJson(string filePath, ListManager<Animal> animalList)
        {
            List<Animal> animals = animalList.List;
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    Converters = new List<JsonConverter> { new StringEnumConverter() },
                    TypeNameHandling = TypeNameHandling.All
                };

                string json = JsonConvert.SerializeObject(animals, settings);
                File.WriteAllText(filePath, json);
            }
            catch(IOException ex)
            {
                throw new Exception("An I/O error occured while saving to a JSON file.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred during file handling.", ex);
            }
        }


        /// <summary>
        /// Retrieve animal data that's in JSON fromat from a file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static AnimalManager ReadFromJson(string filePath) 
        {
            try
            {
               

                string json = File.ReadAllText(filePath);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new StringEnumConverter() },
                    TypeNameHandling = TypeNameHandling.All
                };
                List<Animal> animals = JsonConvert.DeserializeObject<List<Animal>>(json, settings);
                AnimalManager manager = new AnimalManager();
                foreach (Animal animal in animals)
                {
                    manager.Add(animal);
                }
                return manager;
               
            }
            catch(FileNotFoundException ex)
            {
                throw new Exception("The JSON file was not found", ex);
            }
            catch(JsonException ex)
            {
                throw new Exception("Failed to parse the JSON data", ex);
            }
            catch(IOException ex)
            {
                throw new Exception("An I/O error occured while loading from JSON file", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred during file handling.", ex);
            }
        }



        /// <summary>
        /// The name of the food items is saved in an xml file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="foodItems"></param>
        /// <exception cref="Exception"></exception>
        public static void ExportToXml(string filePath, List<FoodItem> foodItems)
        {
            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(List<FoodItem>));

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, foodItems);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred during saving data as XML format.", ex);
            }
        }

        public static List<FoodItem> LoadFromXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<FoodItem>));

                using(StreamReader reader = new StreamReader(filePath))
                {
                   return (List<FoodItem>)serializer.Deserialize(reader);
                }
         
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred during loading data from XML file.", ex);
            }
        }


    }
}
