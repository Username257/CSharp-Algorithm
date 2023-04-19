using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssignment
{
    internal class List<T>
    {
        private const int DefaultCapacity = 5; //기본 크기
        private T[] items; //배열로 구현할 거니까
        private int size; //현재 접근 가능한 목록의 사이즈
        public List()
        {
            //새로운 인스턴스 만들 때 호출되는 애 초기화함
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }

        //인덱서[]
        //어떤 것의 인덱스가 몇 번이냐, 인덱스 몇 번은 무엇을 갖고 있냐

        public int IndexOf(T item)
        {
            return Array.IndexOf(items, item, 0, size);
            //items 안에 item이 어딨는 지 0부터 size까지 찾아
        }

        public T? Find(Predicate<T> match) //함수를 넣을 거니까 Predicate
        {
            //인덱스 번호를 가지고 그 번호의 값을 찾는 거

            //predicate의 리턴 값은 bool임
            //매개변수로 x => x.Contains('2')가 들어올 거임
            //저 x는 변수고.. x에 List가 들어가는 거 같은데?
            //Contains의 반환값은 True/False
            if (match == null) //Contains에 아무것도 안 들어오면 null이겠지
                throw new ArgumentNullException("match"); //예외처리

            for (int i = 0; i < size; i++)
            {
                if (match(items[i])) //items에 i가 있는 지 없는 지
                    return items[i]; 
            }
            return default(T); //i가 없다면 기본 값을 반환한다 <- 왜?

        }
        //Add
        //Remove
        //Find
        //FindIndex
        //Count
    }
}
