using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WildlifeTrackerV3.List;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace WildlifeTrackerV3.FoodItems
{
    
    public class FoodItem 
    {
        private string name;
        private ListManager<string> ingredients;

        //Constructor
        public FoodItem()
        {
            name = string.Empty;
            ingredients = new ListManager<string>();
        }

        //Properties
        [XmlElement("Name")]
        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    name = value;
            }
        }

        [XmlIgnore]
        public ListManager<string> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        /// <summary>
        /// Loops through the list of ingredients and converts them to a single string with the values separated with a comma.
        /// </summary>
        /// <returns>A string with the food item's info (name and ingredients)</returns>
        public override string ToString()
        {
            string ingredientsText = string.Empty;

            for (int i = 0; i < Ingredients.Count; i++)
            {
                ingredientsText += Ingredients[i];

                if (i < Ingredients.Count - 1)
                    ingredientsText += ", ";
            }

            return Name + "    " + ingredientsText;
        }
       
    }
}
