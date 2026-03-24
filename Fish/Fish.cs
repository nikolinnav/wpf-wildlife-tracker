using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerSystem.Fish
{
    public abstract class Fish : Animal
    {
        private string waterType;
        private int finCount;

        //Default constructor
        public Fish() : base()
        { 
            waterType = string.Empty;
        }

        public Fish(string waterType, int finCount) :base()
        {
            this.waterType = waterType;
            this.finCount = finCount;
        }

        //Properties
        public string WaterType
        {
            get { return waterType; }
            set
            {
                if (value != string.Empty)
                    waterType = value;
            }
        }

        public int FinCount
        {
            get { return finCount; }
            set
            {
                if (value > 0)
                    finCount = value;
            }
        }

        public override string GetExtraInfo()
        {
           string textOut = base.GetExtraInfo();
           textOut += $"Water type: {WaterType}\nFin count: {FinCount}\n";
            
            return textOut;
        }

    }
}
