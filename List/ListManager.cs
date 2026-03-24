using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTrackerV3.List
{
    public class ListManager<T> : IListManager<T>
    {
        private List<T> list;

        public ListManager()
        {
            list = new List<T>();
        }

        public List<T> List
        {
            get { return list; }
            set { list = value; }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public T this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public bool Add(T item)
        {
            if(item != null)
            {
                list.Add(item);
                return true;
            }
            return false;
        }

        public bool AddRange(List<T> list)
        {
            if(list != null)
            {
                this.list.AddRange(list);
                return true;
            }
            return false;
        }

        public bool CheckIndex (int index)
        {
            if((index >= 0)  && (index < list.Count))
                return true;

            return false;
        }

        public bool ChangeAt(int index, T item)
        {
            if (CheckIndex(index) && (item != null))
            {
                list[index] = item;
                return true;
            }
            return false;
        }

        public bool RemoveAt(int index)
        {
            if (CheckIndex(index))
            {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAll()
        {
            list.RemoveRange(0, list.Count);
        }

        public T GetAt(int index)
        {
                return list[index];
        }

        public string[] ToStringArray()
        {
            string[] array = new string[list.Count];
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    array[i] = list[i].ToString();
                }
            }
            return array;
        }

        public void JsonSerialize(string fileName)
        {

        }

    }
}
