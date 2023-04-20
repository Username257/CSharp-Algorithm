using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListAssignment
{
    //일단 노드를 만들자
    public class Node<T>
    {
        //내부에서 쓰일 prev주소와 next주소
        internal Node<T> prev;
        internal Node<T> next;
        //노드가 들고있을 데이터
        private T item;

        //읽기전용으로 prev, next -> internal로 선언했으니까
        public Node<T> Prev { get { return prev; } }
        public Node<T> Next { get { return next; } }
        //읽기, item에 값 넣는 것만 되게
        public T Item { get { return item; } set { item = value; } }

        //본체를 가져오고
        internal LinkedList<T> list;
        //본체를 읽기전용으로 한 프로퍼티 List
        public LinkedList<T> List { get { return list; } }

        public Node(T value) 
        {
            //값만 가지고 노드 인스턴스를 만들려할 떄
            this.list = null;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public Node(LinkedList<T> list, T value) 
        {
            //리스트 본체랑 값 가지고 노드 인스턴스를 만들려할 떄
            this.list = list;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public Node(LinkedList<T> list, Node<T> prev, Node<T> next, T value)
        {
            //리스트 본체, 이전 노드, 이후 노드, 값을 갖고 인스턴스를 만들려할 떄
            this.list = list;
            this.prev = prev;
            this.next = next;
            this.item = value;
        }
    }
    //연결리스트 본체
    public class LinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;
        //초기화 안 한 private변수들 읽기전용으로 
        //이 안에서만 초기화할 수 있단 건가?
        public Node<T> First { get { return head; } }
        public Node<T> Last { get { return tail; } }
        public int Count { get { return count; } }
        

        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public Node<T> AddFirst(T value)
        {
            //1.새로 노드 만들기
            Node<T> newNode = new Node<T>(this, value);

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
        public Node<T> AddLast(T value)
        {
            //1.새로 노드 만들기
            Node<T> newNode = new Node<T>(this, value);

            //2. 연결구조 바꾸기
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
        public void Remove(Node<T> node)
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
            Node<T> findNode = Find(value);
            if (findNode != null)
            {
                Remove(findNode);
                return true;
            }
            else
                return false;
        }

        //붙이는 것만 하려면 void 반환형이 있는 이유는 붙인 걸 출력하라고
        public Node<T> AddBefore(Node<T> node, T value)
        {
            if (node.list != this)
                throw new InvalidOperationException();
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            //1.새로운 노드 만들기
            Node<T> newNode = new Node<T>(this, value);
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

        public Node<T> AddAfter(Node<T> node, T value)
        {
            if (node.list != this)
                throw new InvalidOperationException();
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            //1.새로운 노드 만들기
            Node<T> newNode = new Node<T>(this, value);
            //2.연결구조 바꾸기
            newNode.next = node.next;
            newNode.prev = node;
            node.next = newNode;
            if (node.next != null) //node.next != null && node != tail
                node.prev.next = newNode;
            else
                tail = newNode;
            //3.갯수 증가
            count++;

            return newNode;
        }

        public Node<T> Find(T value)
        {
            Node<T> target = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while (target != null) //head부터 끝까지(없을떄까지)
            {
                if (comparer.Equals(value, target.Item))
                    return target;
                else
                {
                    target = target.next;
                }
            }
            return null;
        }
    }
}
