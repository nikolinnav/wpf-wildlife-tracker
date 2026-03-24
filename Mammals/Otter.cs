using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Mammals
{
    public class Otter : Mammal
    {
        private double diveDepth;
        private double bodyLength;
        private MammalSpecies species = MammalSpecies.Otter;

        //Default consturctor
        public Otter() : base() { }

        public Otter(string habitat, int teethCount) :base(habitat, teethCount) 
        {
        }

        //Properties
        public double DiveDepth
        {
            get
            { return diveDepth; }
            set
            {
                if (value > 0)
                {
                    diveDepth = value;
                }
            }
        }

        public double BodyLength
        {
            get { return bodyLength; }
            set
            {
                if (value > 0)
                    bodyLength = value;
            }
        }

        public MammalSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Otter\n";
            textOut += base.GetExtraInfo();
            textOut += $"Dive depth(m): {DiveDepth}\nBody length(cm): {BodyLength}";

            return textOut;
        }
    }
}
