using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Birds
{
    public class Stork : Bird
    {
        private string migrationMonths;
        private double nestSize;
        private BirdSpecies species = BirdSpecies.Stork;

        //Default constructor
        public Stork() : base() 
        {
            migrationMonths = string.Empty;
        }

        public Stork (double wingSpan, double flightSpeed) :base(wingSpan, flightSpeed)
        {
            migrationMonths = string.Empty;
        }

        //Properties
        public string MigrationMonths
        {
            get { return migrationMonths; }
            set
            {
                if (value != "")
                    migrationMonths = value;
            }
        }

        public double NestSize
        {
            get { return nestSize; }
            set
            {
                if (value > 0.0)
                    nestSize = value;
            }
        }

        public BirdSpecies Species
        {
            get { return species; }
        }

        public override string GetExtraInfo()
        {
            string textOut = "Stork\n";
            textOut += base.GetExtraInfo();
            textOut += $"Migration months: {MigrationMonths}\nNest size(m): {NestSize}";

            return textOut;
        }
    }
}
