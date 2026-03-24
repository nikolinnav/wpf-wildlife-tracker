using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Fish
{
    public class WhiteShark : Fish
    {
        private double swimmingDepth;
        private double swimmingSpeed;
        private FishSpecies species = FishSpecies.White_shark;
        //Default constructor
        public WhiteShark() : base() { }

        public WhiteShark(string waterType, int finCount) : base(waterType, finCount) 
        {
        }

        //Properties
        public double SwimmingDepth
        {
            get { return swimmingDepth; }
            set
            {
                if (value > 0.0)
                    swimmingDepth = value;
            }
        }

        public double SwimmingSpeed
        {
            get { return swimmingSpeed; }
            set
            {
                if (value > 0.0)
                    swimmingSpeed = value;
            }
        }

        public FishSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "White shark\n";
            textOut += base.GetExtraInfo();
            textOut += $"Swimming depth(m): {SwimmingDepth}\nSimming speed(km/h): {SwimmingSpeed}";

            return textOut;
        }
    }
}
