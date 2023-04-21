using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05._Queue
{
    internal class Queue<T>
    {
        private const int DefaultCapacity = 4;
        private T[] array;
        private int head;
        private int tail;

        public Queue()
        {
            array = new T[DefaultCapacity + 1];
            head = 0;
            tail = 0;
        }
        public int Count
        {
            get 
            {
                if (head <= tail)
                    return tail - head;
                else
                    return tail - head + array.Length;
            }
        }
        public void Enqueue(T item)
        {
            if (IsFull()) //고전 큐 : 크기 꽉 차면 못 씀 -> 우리는 크기 늘릴 거임
                Grow();

            array[tail] = item;
            MoveNext(ref tail);
            //테일이 끝에 있으면 맨 앞으로 가야하니까
        }
        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            T result = array[head];
            MoveNext(ref head);
            return result;
        }
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException();
            return array[head];
        }
        private void MoveNext(ref int index) //꼬리의 원본이 바껴야하니까
        {
            index = (index == array.Length - 1) ? 0 : index + 1;
        }
        private bool IsEmpty()
        {
            return head == tail;

        }
        private bool IsFull()
        {
            if (head > tail)
                return head == tail + 1;
            else
                return head == 0 && tail == array.Length - 1;
            //head == (tail + 1) % array.Length; 도 되지만 위에가 가독성이 좋다
            //배열이 5칸이고 4번째 칸이 맨 끝이니까 -1
        }
        private void Grow()
        {
            int newCapaity = array.Length * 2;
            T[] newArray = new T[newCapaity];
            if (head < tail)
                Array.Copy(array, newArray, Count);

            else
            {
                Array.Copy(array, head, newArray, 0, array.Length - head);
                Array.Copy(array, 0, newArray, array.Length - head, tail);
                head = 0;
                tail = Count;
            }
            array = newArray;
        }
    }
}
