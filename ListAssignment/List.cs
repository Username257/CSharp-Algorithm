using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListAssignment 
{
    //순차 리스트 특징
    //배열이 쓰임(인덱싱 가능)
    //리스트는 중간이 비어있으면 안 된다(요소끼리 서로 연결되어있어야 한다)
    //아이템 삽입시에 배열은 인덱스 값의 위에 덮어씌우지만, 리스트는 해당 인덱스부터 한 칸씩 뒤로 밀려나고 중간에 삽입된다
    //수용가능 크기에 아이템들이 들어가있음(남을 수 있음)
    //생성 시 크기를 지정하지 않음.크기보다 더 큰 아이템을 수용해야할 경우 수용 가능 크기를 늘림
    //선언 시 자료형 정의
    //데이터 삭제, 삽입 시에 그 인덱스의 뒤의 값들을 당겨오거나 밈(복붙)
    //동적으로 크기 줄이기 어렵다? 늘리는 거랑 똑같이 하면 되지 않나?
    //연결리스트가 추가, 삭제에서 더 효율적이라면 탐색은 순차 리스트가 효율적(인덱싱)
    //add, remove, removeAt, indexOf, grow, find, findIndex, count

    internal class List<T>
    {
        private T[] list = new T[0];
        //요소 담을 수 있는 제네릭 배열
        //new 키워드 == 메모리 할당
        private int defaultSize = 4;
        //기본 사이즈 근데 왜 상수로 해 놓을까? private으로 선언하고 클래스 내에서도 안 건들면 되지만
        //가독성을 위해서???
        private int itemNum = 0;
        //담겨있는 요소 갯수
        public List()
        {
            list = new T[defaultSize];
            itemNum = 0;
            //생성자에서 초기화 하는 거랑 클래스 자체에서 초기화하는 거랑 다른 점은?
            //this는 외부변수랑 내부변수 이름이 같을 때만 쓰는 듯
        }
        //인스턴스 선언 시 초기화할 값들

        public T this[int index]
        {
            get 
            {
                if (index < 0 || index >= itemNum)
                    throw new IndexOutOfRangeException(); 
                else
                    return list[index]; 
                //예외처리 잊지 말기!
                
            }
            set
            {
                if (index < 0 || index >= itemNum)
                    throw new IndexOutOfRangeException();
                else
                    list[index] = value;
            }
        } //인덱스 접근 가능하게 인덱서로 해주기

        public void Add(T item)
        {
            if (itemNum == defaultSize) //꽉 차면 키우기
            {
                Grow();
                list[itemNum] = item;
                itemNum++;
            }
            else
            {
                list[itemNum] = item;
                //길이는 1부터 시작하고 인덱스는 0부터 시작하니까
                itemNum++;
            }
        }
        public void Remove()
        {
            //끝에 있는 애 지우면 되니까
            if (itemNum == 0)
                throw new IndexOutOfRangeException();
            else
            {
                //list[itemNum] = default(T);
                //근데 굳이 디폴트값 안 넣어도 그 인덱스 자리 쓰지 못하게 막으면 되니까
                itemNum--;
            }
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= itemNum) //아이템 갯수는 1부터 인덱스는 0부터
                throw new IndexOutOfRangeException();
            else
            {
                //그 자리에 있는 걸 지우면 뒤에있는 걸 앞 당겨와야함
                T[] newArray = new T[defaultSize];
                //복사하기 전에 새로운 애
                Array.Copy(list, 0, newArray, 0, index - 1);
                //sourceArray, sourceIndex, destinationArray, destinationIndex, length
                //일단 0 ~ 인덱스까지 복사를 하면 인덱스 자리도 같이 복사되니까 빼주기
                Array.Copy(list, index + 1, newArray, index, itemNum - index);
                //인덱스가 2면 3부터 현재 요소 갯수가 4면 - 2 = 2개만큼 복사해라 3 ~ 4번이 복사되겠지?
                //인덱스가 빠진 곳 앞 뒤로 복붙했는데 이럴 필요 없었음
                //걍 기존 배열에다 복사하면 되는 거였네
            }
        }
        public int IndexOf(T item)
        {
            //Array.IndexOf 안 쓰고 구현해보기
            //하려 했으나 아이템의 자료형과 배열의 자료형이 안맞을 수 있어서 ==을 지원 안 해주는 듯?
            return Array.IndexOf(list, item, 0, itemNum);
            //array, value, startIndex, count(검색 할 요소의 수)
        }
        public void Grow()
        {
            int newSize = itemNum * 2;
            T[] newList = new T[newSize];
            list = newList;
        }
        public T? Find(Predicate<T> match)
        {
            //특정 조건(match) 맞는 첫 번째 요소를 반환 없으면 기본값 반환
            //T[] array 없는 이유 == 쓸 때 리스트 인스턴스.Find하지 않을까?
            //뭐 예를 들어 x => x == 5가 들어온다고 생각 해보자
            for (int i = 0; i < itemNum; i++)
            {
                if (match == null)
                    throw new Exception();
                //예외처리..
                if (match(list[i]))
                {
                    return list[i];
                }
            }
            //for문 돌고 없으면
            return default(T);
        }
        public int FindIndex(Predicate<T> match)
        {
           //지정된 조건자에 정의된 조건과 일치하는 요소 검색하고
           //조건 일치하는 첫 번째 요소의 인덱스를 반환
           //find랑 같은데 인덱스만 주면 되는 거 아님? 없는 경우 -1 반환
            for(int i = 0; i < itemNum; i++)
            {
                if (match == null)
                    throw new Exception();
                if (match(list[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public int count { get { return itemNum; } }
    }
}
