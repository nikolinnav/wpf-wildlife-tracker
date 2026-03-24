using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Animals;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Animals
{
    [Serializable]
    public class Animal : IAnimal
    {
        private string name;
        private double age;
        private string id = string.Empty;

        //No additional logic is needed for the set properties in Category and Gender, so I opt for auto-implemented properties.
        public Category Category { get; set; }
        public Gender Gender { get; set; }

        /// <summary>
        /// Default constructor 
        /// </summary>
        public Animal()
        {
            name = string.Empty;
            Category = Category.Mammal;
            Gender = Gender.Unknown;
        }


        public Animal (string name, double age, Category category, Gender gender)
        {
            this.name = name;
            this.age = age;
            this.Category = category;
            this.Gender = gender;
        }

        //Properties
        public string Name
        {
            get { return name; }
            set
            {
                if (value != "")
                    name = value;
            }
        }

        public double Age
        {
            get { return age; }
            set
            {
                if(value >= 0.0)
                    age = value;
            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    id = value;
            }
        }

        public virtual bool ValidateInput(string name, double age)
        {
            bool ok = false;
            if (!string.IsNullOrEmpty(name) && (age > 0.0))
                ok = true;

            return ok;
        }

        /// <summary>
        /// Every animal gets a unique ID that consists of the first letter of the animal's category
        /// and the number of the animal in the list.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public string GetAnimalID(Category category, int id)
        {
            string animalId = string.Empty;
            switch (category)
            {
                case Category.Mammal:
                    animalId = $"M{id.ToString()}";
                    id++;
                    break;
                case Category.Bird:
                    animalId = $"B{id.ToString()}";
                    id++;
                    break;
                case Category.Fish:
                    animalId = $"F{id.ToString()}";
                    id++;
                    break;
            }


            return animalId;
        }

        //Get the Category specific information as a string.
        public virtual string GetExtraInfo()
        {
            string textOut = $"Category:  {Category}\n\n";
            return textOut;
        }

    }
}
