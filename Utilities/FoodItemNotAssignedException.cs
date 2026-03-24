using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTrackerV4.Utilities
{
    class FoodItemNotAssignedException :Exception
    {
        public FoodItemNotAssignedException(string foodName) 
            :base($"No animals are assigned to the food item: {foodName}") {}
    }
}
