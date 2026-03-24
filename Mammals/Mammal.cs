using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerV2.Nutrition;


namespace WildlifeTrackerSystem.Mammals
{
    public abstract class Mammal : Animal
    {
        private string habitat;
        private int teethCount;

        //Default constructor
        public Mammal () : base()
        {
            habitat = string.Empty;
        }

        public Mammal (string habitat, int teethCount) : base()
        {
            this.habitat = habitat;
            this.teethCount = teethCount;
        }

        //Properties
        public string Habitat
        {
            get { return habitat; }
            set
            {
                if (value != "")
                    habitat = value;
            }
        }

        public int TeethCount
        {
            get { return teethCount; }
            set
            {
                if (value > 0)
                    teethCount = value;
            }
        }


        public override string GetExtraInfo()
        {
            string textOut = base.GetExtraInfo();
            textOut += $"Habitat: {Habitat}\nTeeth Count: {TeethCount}\n";

            return textOut;
        }
    }
}
