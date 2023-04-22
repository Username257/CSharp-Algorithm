using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._Iterator
{
    public class LinkedListNode<T>
    {
        internal LinkedList<T> list;
        internal LinkedListNode<T> prev;
        internal LinkedListNode<T> next;
        private T item;

        public LinkedListNode(T value)
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
        {
            this.list = list;
            this.prev = prev;
            this.next = next;
            this.item = value;
        }

        public LinkedList<T> List { get { return list; } }
        public LinkedListNode<T> Prev { get { return prev; } }
        public LinkedListNode<T> Next { get { return next; } }

        public T Item { get { return item; } set { item = value; } }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        private LinkedListNode<T> head;
        private LinkedListNode<T> tail;
        private int count;

        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return tail; } }
        public int Count { get { return count; } }

        public LinkedListNode<T> AddFirst(T value)
        {
            //1.새로 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            //2. 연결구조 바꾸기
            if (head != null)
            {
                //2-1. head의 노드가 있었을 때
                newNode.next = head;
                head.prev = newNode;
                head = newNode;
            }
            else //2-2. head 노드가 없었을 떄 == 연결리스트에 아무것도 없었을 때
            {
                //새로 만들고 집어넣자마자 걔를 head이자 tail로
                head = newNode;
                tail = newNode;
            }

            //3. 갯수늘리기
            count++;
            return newNode;
        }
        //AddLast는 과제
        //링크드 리스트 기술면접 조사
        //기존에 있던 노드를 다른 곳에 붙이는 건 안 된다
        //이미 다른 연결리스트에 포함되어 있는 노드는 추가 불가
        public LinkedListNode<T> AddLast(T value)
        {
            //1.새로 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            //2. 연경구조 바꾸기
            if (tail != null)
            {
                //2-1. tail의 노드가 있었을 때
                newNode.prev = tail;
                tail.next = newNode;
                tail = newNode;
            }
            else //2-2. head 노드가 없었을 떄 == 연결리스트에 아무것도 없었을 때
            {
                //새로 만들고 집어넣자마자 걔를 head이자 tail로
                head = newNode;
                tail = newNode;
            }

            //3. 갯수늘리기
            count++;
            return newNode;
        }
        public void Remove(LinkedListNode<T> node)
        {
            //예외 : node가 linkedList에 포함된 노드가 아닌 경우
            if (node.list != this) //노드가 내거
                throw new InvalidOperationException();
            //예외2 : 노드가 null인 경우
            if (node == null)
                throw new ArgumentNullException();
            //지웠을 때 head나 tail이 변경되는 경우
            if (head == node)
                head = node.next;
            if (tail == node)
                tail = node.prev;
            //연결 구조 바꾸고 지우자
            if (node.prev != null)
                node.prev.next = node.next;
            if (node.next != null)
                node.next.prev = node.prev;
            //둘 다 바꿔야하니까 else 아니고 if
            //갯수 줄이기
            count--;
            //왜 노드를 null로 안만드냐 - 어차피 가리키는 애 없어서 사라짐 해도 되고 안 해도 돼
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> findNode = Find(value);
            if (findNode != null)
            {
                Remove(findNode);
                return true;
            }
            else
                return false;
        }

        //붙이는 것만 하려면 void 반환형이 있는 이유는 붙인 걸 출력하라고
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            if (node.list != this)
                throw new InvalidOperationException();
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            //1.새로운 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            //2.연결구조 바꾸기
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev = newNode;
            if (node.prev != null) //node.prev == null : node == head
                node.prev.next = newNode;
            else
                head = newNode;
            //3.갯수 증가
            count++;

            return newNode;
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> target = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while (target != null) //head부터 끝까지(없을떄까지)
            {
                if (comparer.Equals(value, target.Item))
                    return target;
                else
                    target = target.next;
            }
            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this); //구조체(반복자) 생성 가져오기
        }

        public struct Enumerator : IEnumerator<T>
        {
            private LinkedListNode<T> node;
            private LinkedList<T> linkedList;
            private T current;
            public Enumerator(LinkedList<T> linkedList)
            {
                this.linkedList = linkedList;
                this.node = linkedList.head;
                this.current = default(T);
            }

            public T Current {get {return current;}}

            object IEnumerator.Current { get { return current; } }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                if(node != null)
                {
                    current = node.Item;
                    node = node.next;
                    return true;
                }
                else
                {
                    current = default(T);
                    return false;
                }
            }

            public void Reset()
            {
                node = linkedList.head;
                current = default(T);
            }
        }
    }
}
