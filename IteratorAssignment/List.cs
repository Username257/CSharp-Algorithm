using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._Iterator
{
    public class List<T> : IEnumerable<T> //인터페이스 붙이면 이 자료구조는 반복할 수 있게 됨                                
    {
        private const int DefaultCapacity = 10;

        private T[] items;
        private int size;
        private int position = -1; //현재 위치
        public int Position { get { return position; } }
        public List()
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }

        public int Capacity { get { return items.Length; } }
        public int Count { get { return size; } }

        public T this[int index] //인덱서
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





        /* 반복기 내 맘대로 구현해보기 */
        //반복기 구현해야 할 기능 : 현재 값 알려주는 애, 다음으로 갈 수 있는 애, 처음으로 돌아가는 애
        // object? Current {get;}
        // bool MoveNext();
        // void Reset();

        //1. IEnumerable<T> 인터페이스 붙이기
        //2. GetEnumerator() 메서드 구현하기 -> 열거자 반환
        //3. Current 프로퍼티, MoveNext, Reset 메서드 구현

        

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            List<T> list = new List<T>();

            public Enumerator(List<T> list)
            {
                this.list = list;
            }

            public T Current
            {
                get
                {
                    return list.items[list.position]; //맨 처음엔 -1번째 방 출력 될텐데
                }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
          
                    //다음으로 이동 가능하면 true 반환하는 거
                if (list.position < list.Count)
                {
                    //리스트의 길이와 현재 위치가 같거나 크다면(클 일이 있나?)
                    Reset(); //처음으로 돌아가기
                    return false;
                }
                else
                {
                    list.position++; //현재 위치는 다음으로 옮겨주고
                    return true;
                }
                //예외 처리가 안 됐다는데..
                //list.position >= list.items.Length - 1
            }
            public void Reset()
            {
                list.position = -1;
                /*
                 * 맨 처음을 0이 아니라 -1로 설정하는 이유
                 * while(iter.MoveNext()) MoveNext하고 살펴보기 때문에
                 * prevIterator 개념을 써서 -1을 맨 처음으로 잡아줘야함
                 * 다른 개념으로는 afterIterator로 마지막 +1 공간을 두는 애도 있음
                 */
            }
        }
    }
}
