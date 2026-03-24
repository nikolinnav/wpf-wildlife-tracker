using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Fish
{
    public class OrangeClownfish : Fish
    {
        private double sizeAtMaturity;
        private int eggsLayed;
        private FishSpecies species = FishSpecies.Orange_clownfish;

        //Default constructor
        public OrangeClownfish() : base() { }

        public OrangeClownfish(string waterType, int finCount) : base(waterType, finCount)
        {
        }

        //Properties
        public double SizeAtMaturity
        {
            get { return sizeAtMaturity; }
            set
            {
                if (value > 0)
                    sizeAtMaturity = value;
            }
        }

        public int EggsLayed
        {
            get { return eggsLayed; }
            set
            {
                eggsLayed = value;
            }
        }

        public FishSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Orange ClownFish\n";
            textOut += base.GetExtraInfo();
            textOut += $"Size at maturity(cm): {SizeAtMaturity}\nEggs layed: {EggsLayed}";

            return textOut;
        }

    }
}
