using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._Iterator
{
    internal class List<T> : IEnumerable<T> //인터페이스 붙이면 이 자료구조는 반복할 수 있게 됨
                                            //== 반복기를 가지고 있다 == GetEnumerator 구현
    {
        private const int DefaultCapacity = 10;

        private T[] items;
        private int size;

        public List()
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }

        public int Capacity { get { return items.Length; } }
        public int Count { get { return size; } }

        public T this[int index] 
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

                return items[index];
            }
            set
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

                items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (size < items.Length)
                items[size++] = item;
            else
            {
                Grow();
                items[size++] = item;
            }
        }
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else
                return false;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException();

            size--;
            Array.Copy(items, index + 1, items, index, size - index);
        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(items, item, 0, size);
        }
        private void Grow()
        {
            int newCapacity = items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
        }

        public T? Find(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return i;
            }
            return -1;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            private int index; //리스트의 반복기는 인덱스를 알고 있어야함
            private List<T> list;//내가 어떤 리스트를 가리키고 있는 지도
            private T current;
            public T Current { get { return current; } }

            public Enumerator(List<T> list)
            {
                this.list = list;
                this.index = -1; //moveNext시 첫 번째가 나오기 위해
                this.current = default(T); //아무것도 안 가리킬 떄 / 잘못된 것을 가리킬 떄 0
            }
            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                if (index < list.Count - 1) //갯수는 4고 인덱스는 3일때 인덱스 2까지만
                {
                    current = list[index++];//후위 증감 연산자가 주고 다음 거 가능하게 한다
                    return true;
                }
                else
                {
                    current = default(T); //못 주는 상황에선 current를 default로 돌려놓음
                    index = list.Count;
                    return false;         //마지막 애 값을 갖고 있으면 안 되니까
                }
            }

            public void Reset()
            {
                index = -1; //맨처음이 0이 아니라 -1 에서 moveNext하면 0번째 되게
                current = default(T);
            }

            //반복기 -> 자료구조의 내용물에 무관하게 쓸 수 있는 기능
        }
    }
}
