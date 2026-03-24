using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;

namespace WildlifeTrackerV2.Animals
{
    public class AnimalSpeciesComparer : IComparer<Animal>
    {
        public int Compare(Animal x, Animal y)
        {
            if(x == null || y == null)
                return 0;

            return x.Category.CompareTo(y.Category);
        }
    }
}
