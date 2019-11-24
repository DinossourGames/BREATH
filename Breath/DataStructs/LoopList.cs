using System.Collections.Generic;
using Breath.Abstractions.Interfaces;

namespace Breath.DataStructs
{
    public struct LoopList<T> where T : ISelectable
    {
        private List<T> Items { get; set; }
        private int _index;

        public LoopList(List<T> items)
        {
            _index = 0;
            Items = items;
            Items[_index].Select();

        }

        public T GetItem() => Items[_index];
        public void AddItem(T item) => Items.Add(item);
        public bool RemoveItem(T item) => Items.Remove(item);

        public T NextItem()
        {
            Items[_index].Select();
            _index++;

            if (_index  >= Items.Count)
                _index = 0;
            

            Items[_index].Select();
            return Items[_index];
        }

        public T PreviousItem()
        {
            Items[_index].Select();
            _index--;
            
            if (_index  < 0)
                _index = Items.Count -1;
        
            Items[_index].Select();
            return Items[_index];
        }
    }
}