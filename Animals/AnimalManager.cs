using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerV3.FoodItems;
using WildlifeTrackerV3.List;
using WildlifeTrackerV4.Utilities;

namespace WildlifeTrackerV2.Animals
{
    [Serializable]
    public class AnimalManager : ListManager<Animal>
    {
        
        private int startID = 0;

        //List of FoodItem objects that are greated with the help of FoodWindow.
        private List<FoodItem> foodItems = new List<FoodItem>();

        //A dictionary that connects a food item with a list of animals. 
        private Dictionary<FoodItem, List<Animal>> animalFood = new Dictionary<FoodItem, List<Animal>>();

        public AnimalManager()
        {
            
        }

        //properties
        public List<FoodItem> FoodItems 
        {
            get { return foodItems; } 
            set { foodItems = value; }
        }

        public Dictionary<FoodItem, List<Animal>> AnimalFood
        {
            get
            { return animalFood;}
        }

        /// <summary>
        /// Sets an ID for the animal and adds it to the Animal List.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns>bool if the animal object is successfully added, false otherwise.</returns>
        public bool AddAnimal (Animal animal)
        {
            if (animal != null)
            {
                string animalId = animal.GetAnimalID(animal.Category, startID);
                animal.Id = animalId;
                Add(animal);
                startID++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method is used in the Change animal event listener.
        /// It determines the number of the animal object in the list and sets the first letter that is adequate for the category.
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="newAnimal"></param>
        /// <returns></returns>
        public bool PreserveAnimalID(Animal newAnimal, int index)
        { 
            if (newAnimal != null)
            {
                if (newAnimal.Category == Category.Mammal)
                    newAnimal.Id = $"M{index.ToString()}";
                else if (newAnimal.Category == Category.Bird)
                    newAnimal.Id = $"B{index.ToString()}";
                else
                    newAnimal.Id = $"F{index.ToString()}";

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks wheather the given key already exists, if so, it adds the animal object in the list of values.
        /// if the key doesn't exist, it creates a new key-value pair. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddKeyValuePair (FoodItem key, Animal value)
        {
            if (key == null)
                return false;
            if (value == null)
                throw new FoodItemNotAssignedException(key.Name);

            if (animalFood.ContainsKey(key))
            {
                animalFood[key].Add(value);
                return true;
            }
            else if (!animalFood.ContainsKey(key))
            {
                animalFood[key] = new List<Animal> { value };
                return true;
            }

            return false;
        }
       
        public string[] GetAnimalInfoStrings()
        {
            string[] infoStrings = new string[Count];
            for (int i = 0; i < infoStrings.Length; i++)
            {
                string animalId = List[i].GetAnimalID(List[i].Category, startID);
                List[i].Id = animalId;
                infoStrings[i] = List[i].GetExtraInfo();
            }

            return infoStrings;
        }


        public void SortByName()
        {
            List.Sort(new AnimalNameComparer());
        }

        public void SortByCategory()
        {
            List.Sort(new AnimalSpeciesComparer());
        }
    }
}
