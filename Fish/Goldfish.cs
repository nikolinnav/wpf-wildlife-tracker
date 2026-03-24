using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Fish
{
    public class Goldfish : Fish
    {
        private double waterTemperature;
        private string color;
        private FishSpecies species = FishSpecies.Goldfish;

        //Default constructor
        public Goldfish() : base() 
        {
            color = string.Empty;
        }

        public Goldfish(string waterType, int finCount) :base(waterType, finCount)
        {
            color = string.Empty;
        }

        //Properties
        public double WaterTeperature
        {
            get { return waterTemperature; }
            set
            {
                if (value > 0.0)
                    waterTemperature = value;
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                if (value != "")
                    color = value;
            }
        }

        public FishSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Goldfish\n";
            textOut += base.GetExtraInfo();
            textOut += $"Water Temperature(C): {WaterTeperature}\nColor: {Color}";

            return textOut;
        }
    }
}
