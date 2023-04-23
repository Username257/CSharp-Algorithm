using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackQueueAssignment
{
    /*
     * 스택은 리스트랑 유사한 점이 많아서 리스트로 구현함(어댑터)
     */
    internal class AdapterStack<T>
    {
        private List<T> container; //리스트로 구현
        public AdapterStack()
        {
            container = new List<T>();
        } //생성자 써주고
        //push, pop, peek 구현
        public void Push(T item)
        {
            //아이템을 받으면
            container.Add(item); //아이템 추가
            //리스트니까 수용가능 크기 그런 거 없음

        }
        public T Pop()
        {
            T item = container[container.Count - 1];
            container.RemoveAt(container.Count - 1);
            /* container.Count - 1 인 이유 크기는 1부터, 인덱스는 0부터라서 */
            return item; //일단 팝 한 아이템을 줌
        }
    }
}
