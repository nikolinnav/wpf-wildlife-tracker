using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTrackerSystem.Animals;
using WildlifeTrackerV2.Nutrition;

namespace WildlifeTrackerV2.Animals
{
    public interface IAnimal
    {
        string Name { get; set; }
        string Id { get; set; } 
        Gender Gender { get; set; }
        string GetExtraInfo();

    }
}
