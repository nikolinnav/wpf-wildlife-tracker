using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Birds
{
    public class Falcon : Bird
    {
        private string nestingSeason;
        private double beakLength;
        private BirdSpecies species = BirdSpecies.Falcon;

        //Deafult constructor 
        public Falcon() : base() 
        {
            nestingSeason = string.Empty;
        }

        public Falcon (double wingSpan, double flightSpeed) : base(wingSpan, flightSpeed)
        {
            nestingSeason = string.Empty;
        }

        //Properties
        public string NestingSeason
        {
            get { return nestingSeason; }
            set
            {
                if (value != "")
                    nestingSeason = value;
            }
        }

        public double BeakLength
        {
            get { return beakLength; }
            set
            {
                if (value > 0.0)
                    beakLength = value;
            }
        }

        public BirdSpecies Species 
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Falcon\n";
            textOut += base.GetExtraInfo();
            textOut += $"Nesting season: {NestingSeason}\nBeak length(cm): {BeakLength}";
             
            return textOut; 
        }
    }
}
