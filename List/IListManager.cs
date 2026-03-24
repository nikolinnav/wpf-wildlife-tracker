using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WildlifeTrackerV3.List
{
    public interface IListManager<T>
    {
        //Returns the number of elements in the list.
        int Count { get; }

        //Adds an Item to the list and returns true if it is added successfully, false otherwise.
        bool Add(T item);

        //Checks if the index of a certain element is within the range of the list.
        bool CheckIndex(int index);

        //Changes an item at a given index in the list. Firstly, it should check if the index is within the range of the list.
        bool ChangeAt(int index, T item);

        //Deletes an item at a given index from the list. Firstly, it should check if the index is within the range of the list.
        bool RemoveAt(int index);

        //Deletes all items from the list.
        void RemoveAll();

        //Returns an item at a given index. Firstly, it should check if the index is within the range of the list. 
        T GetAt(int index);

        //Returns an array of strings that represent information about the list items.
        string[] ToStringArray();

        void JsonSerialize(string fileName);


    }
}
