using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Mammals
{
    public class Cat : Mammal
    {
        private string breed;
        private string furColor;
        private MammalSpecies species = MammalSpecies.Cat;

        //Default constructor
        public Cat() : base()
        {
            breed = string.Empty;
            furColor = string.Empty;
        }

        public Cat(string habitat, int teethCount) :base(habitat, teethCount)
        {
            breed = string.Empty;
            furColor = string.Empty;
        }

        //Properties
        public string Breed
        {
            get { return breed; }
            set
            {
                if (value != "")
                    breed = value;
            }
        }

        public string FurColor
        {
            get { return furColor; }
            set
            {
                if (value != "")
                    furColor = value;
            }
        }

        public MammalSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Cat\n";
            textOut += base.GetExtraInfo();
            textOut += $"Breed: {Breed}\nFur Color: {FurColor}";

            return textOut;
        }
    }
}
