using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;

namespace WildlifeTrackerV2.Animals
{
    public class AnimalNameComparer : IComparer<Animal>
    {
        public int Compare(Animal x, Animal y)
        {
            if (x == null || y == null)
                return 0;

            return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
