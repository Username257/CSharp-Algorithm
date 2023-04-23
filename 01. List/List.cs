using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    //internal 같은 어셈블리(프로젝트)에선 public, 다른 어셈블리에선 private
    //도움 클래스 등을 만드는데 주로 사용
    internal class List<T>
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
            //0번째에 아이템 넣고 나중에 ++
            else
            {
                Grow();
                items[size++] = item;
            }
        }
        public bool Remove(T item)
        {
            //지우고 뒤에서부터 하나씩 당겨오니까 n번의 연산
            //최선의 상황 : 맨 뒤에거 지우는 경우
            int index = IndexOf(item);
            if (index >= 0)
            {
                // 찾았을 경우
                RemoveAt(index);
                return true;
            }
            else
                // 못 찾았을 경우
                return false;
        }
        public void RemoveAt(int index)
        {
            if (index< 0 || index >= size)
                throw new IndexOutOfRangeException();

            size--;
            Array.Copy(items, index + 1, items, index, size - index);
            //index의 뒤에 있는 방부터 인덱스 ~ 사이즈 - 인덱스(하나씩 당겨온다)
            //하나씩 당겨오면 맨 마지막 하나는 그냥 남는데 걔는 안 쓸 거라 상관없음
            //size만큼만 사용 다음 번 Add시 덮어씌움
        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(items, item, 0, size);
        }
        private void Grow()
        {
            int newCapacity = items.Length * 2;
            //꽉 찼다면 2배로 넓게
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
            //지역변수에 item, newItems가 있음
            //items가 newItems가 가리키고 있는 주소를 가리키게 함
            //그러면 newItems가 사라지고 items가 원래 있던 주소가 사라짐
            //으로써 완벽 복사 성공!
        }

        //없을 수도 있으니까 Nullable
        public T? Find(Predicate<T> match) //(x => x.Contains('4')가 match에 들어옴
        {
            if (match == null)
                throw new ArgumentNullException("match");

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);
            //뒤에오는 자료형의 기본 값 반환
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
    }
}
