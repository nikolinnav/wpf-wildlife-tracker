using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Mammals
{
    public class Tiger : Mammal
    {
        private string landOfOrigin;
        private string conservationStatus;
        private MammalSpecies species = MammalSpecies.Tiger;

        //Default constructor
        public Tiger() : base()
        {
            landOfOrigin = string.Empty;
            conservationStatus = string.Empty;
        }

        public Tiger(string habitat, int teethCount) : base(habitat, teethCount)
        {
            landOfOrigin = string.Empty;
            conservationStatus = string.Empty;
        }
        //Properties
        public string LandOfOrigin
        {
            get { return landOfOrigin; }
            set
            {
                if (value != "")
                    landOfOrigin = value;
            }
        }

        public string ConservationStatus
        {
            get { return conservationStatus; }
            set
            {
                if (value != "")
                    conservationStatus = value;
            }
        }

        public MammalSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Tiger\n";
            textOut += base.GetExtraInfo();
            textOut += $"Land of origin: {LandOfOrigin}\nConservation status: {ConservationStatus}";

            return textOut;
        }
    }
}
