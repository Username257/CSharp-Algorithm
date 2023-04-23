using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackQueueAssignment
{
    internal class Queue<T>
    {
        private const int DefaultCapacity = 4; //배열이고 크기 정해져있으니까 처음 크기
        private T[] array; //큐의 본체
        private int head; //움직이고 뺴는 애
        private int tail; //움직이고 넣는 애

        public Queue()
        {
            array = new T[DefaultCapacity + 1]; //tail과 head가 겹쳤을 때 비어있는 지 꽉 채워있는 지를 구분하기 위해
            head = 0;
            tail = 0;
        }

        public int Count //들어간 아이템 갯수
        {
            get
            {
                if (head <= tail) // head ~ tail로 아이템이 들어가있을 때
                    return tail - head; 
                else // tail ~ head 로 아이템이 들어가있을 때
                    return tail - head + array.Length;
                //tail - head해서 빈칸 구하고 array.Length 더해주면 채워져있는 부분 구하기 완료 
            }
        }
        public void Enqueue(T item)
        {
            if (IsFull()) //꽉 찼으면 크기 늘리기
                Grow();

            array[tail] = item; //꼬리가 현재 있는 곳에 아이템 삽입?
            MoveNext(ref tail); //tail의 주소를 준다
            //테일이 끝에 있으면 맨 앞으로 가야하니까
            //넣고 움직이게 구현했다 넣을 공간을 가리키게 했다
        }
        private void MoveNext(ref int index) // 원본이 바껴야하니까
        {
            index = (index == array.Length - 1) ? 0 : index + 1;
            //배열 크기에서 -1 한 자리에 있다면 0으로 돌아가고, 아니면 1만큼 움직여라
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            T result = array[head]; //결과로 줄 거
            MoveNext(ref head); //head가 있는 곳은 빈 공간이 됨
            return result;
        }

        private bool IsFull()
        {
            //다 비어있는 지 다 채워져있는 지 알기 위한 추가부분만 비어져있다면
            if (head > tail) //h가 t보다 뒤에 있다면
                return head == tail + 1; //t에 1갔을 때 h라면 둘이 만나니까 full
            else
                return head == 0 && tail == array.Length - 1; //h가 t보다 앞에 있다면
            //h가 0이고 t가 길이보다 1개 적다면 == 추가 부분이 비어져있다면 꽉 찬 상태
            //배열이 5칸이고 4번째 칸이 맨 끝이니까 -1
        }
        private void Grow()
        {
            int newCapacity = array.Length * 2; //공간을 2배 늘리고
            T[] newArray = new T[newCapacity]; //그만큼의 사이즈를 가진 배열을 생성하고
            if (head < tail) //t가 h보다 큰 인덱스에 있다 h ~ t 에 아이템들이 있다
                Array.Copy(array, newArray, Count); //그냥 복사해주면 됨
            else //아니다 t ~ h 에 아이템들이 있다
            {
                //Array.Copy(복사할 Array, 복사 시작되는 인덱스, 붙여넣기할 Array, 붙여넣기 시작할 인덱스, 복사할 요소 갯수)
                Array.Copy(array, head, newArray, 0, array.Length - head); 
                //h와 그 앞에 있는 애들 복사
                Array.Copy(array, 0, newArray, array.Length - head, tail);
                //t와 그 뒤에 있는 애들 복사
                head = 0; //h 돌려넣기
                tail = Count; // t을 h보다 큰 인덱스에 두기(요소들의 끝)
            }
            array = newArray; //다 하고 배열 적용
        }
        private bool IsEmpty()
        {
            return head == tail;
            //h 랑 t가 겹쳐지는 순간이 다 비어져있는 순간
        }
        public T Peek()
        {
            /* 시작 부분의 개체를 반환 */
            if (IsEmpty())
                throw new InvalidOperationException();
            return array[head]; 
        }
    }
}
