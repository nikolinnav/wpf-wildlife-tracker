using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Birds
{
    public abstract class Bird : Animal
    {
        private double wingSpan;
        private double flightSpeed;

        public Bird() : base() { }

        public Bird(double wingSpan, double flightSpeed)
        {
            this.wingSpan = wingSpan;
            this.flightSpeed = flightSpeed;
        }

        //Properties
        public double WingSpan
        {
            get { return this.wingSpan; }
            set
            {
                if (value > 0.0)
                    this.wingSpan = value;
            }
        }

        public double FlightSpeed
        {
            get { return this.flightSpeed; }
            set
            {
                if (value > 0.0)
                    this.flightSpeed = value;
            }
        }


        public override string GetExtraInfo()
        {
            string textOut = base.GetExtraInfo();
            textOut += $"Wing span(m): {WingSpan}\nFlightSpeed(km/h): {FlightSpeed}\n";
            return textOut;
        }
    }
}
