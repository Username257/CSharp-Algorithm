using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    //이진트리는 왼쪽과 오른쪽의 주소가 둘 다 필요하겠지
    //c#은 노드기반 피하니까 번호를 지정해주면 배열에 담아줘도 됨
    //0을 부모로 둔 1, 2 / 1을 부모로 둔 3, 4 / 2를 부모로 둔 5, 6
    //왼쪽으로 가려면 자신 index * 2 + 1하면 자기 왼쪽 나옴
    //오른쪽자식은 index * 2 + 2
    //부모로 가고 싶은 경우는 (index - 1) / 2 (소숫점 버림)
    //노드기반이지만 구현은 배열로!

    internal class PriorityQueue<TElement>
    {
        private struct Node
        {
            public TElement element;
            public int priority;
            
        }

        private List<Node> nodes; //계속 추가 되는 리스트 어댑터로 구현

        public PriorityQueue()
        {
            this.nodes = new List<Node>();
        }
        

        /* 힙에서 데이터 추가할 경우에 어케 해야하냐 일단 맨 끝 다음 인덱스에 놓음
           부모랑 비교해서 우선순위가 더 높으면 자리 바꾸는 방식으로 자리 잡음 */
        public int Count {  get { return nodes.Count; } }

        
        public void Enqueue(TElement element, int priority)
        {
            Node newNode = new Node() { element = element, priority = priority }; 
            //this 없는 이유 : 문법 생성자랑 하는 일은 똑같다. 그냥 만들자마자 초기화하는 거

            //1. 가장 뒤에 추가
            nodes.Add(newNode); //Add는 list
            int newNodeIndex = nodes.Count - 1; //인덱스는 0부터 시작하니까

            //2. 새로운 노드를 힙 상태가 유지되도록 승격 작업 반복
            while (newNodeIndex > 0)
            {
                //0되면 비교할 대상이 없으니까
                
                //2-1. 부모 노드 확인
                int parentIndex = GetParentIndex(newNodeIndex);
                Node parentNode = nodes[parentIndex];

                //2-2. 자식노드가 부모노드보다 우선순위가 높으면 교체
                if(newNode.priority < parentNode.priority)
                {
                    //To Do : 여기 이해하기(녹강 듣기)
                    nodes[newNodeIndex] = parentNode;
                    nodes[parentIndex] = newNode;
                    newNodeIndex = parentIndex; //TODO : 그럼 parentIndex는 안 바뀌는 거 아닌가?
                }
                else
                    break; 
            }
        }
        public TElement Dequeue()
        {
            Node rootNode = nodes[0];

            // 빠진 후에 힙 상태 유지
            //1. 가장 마지막 노드를 최상단으로 위치
            Node lastNode = nodes[nodes.Count - 1];
            nodes[0] = lastNode;
            nodes.RemoveAt(nodes.Count - 1);

            int index = 0;
            //0번 위치에 있던 노드가 제 위치로 가게끔
            //2. 자식 노드들과 비교하여 더 작은 자식과 교체 반복
            while(index < nodes.Count)
            {
                //자식이 둘, 자식이 하나, 자식이 없는 케이스가 있음
                int leftChildIndex = GetLeftChildIndex(index);
                int rightChildIndex = GetRightChildIndex(index);

                //자식 둘 다
                if (rightChildIndex < nodes.Count) 
                {
                    //왼쪽 자식과 오른쪽 자식을 비교하여 더 우선순위가 높은 자식을 선정
                    int lessChildIndex = nodes[leftChildIndex].priority < nodes[rightChildIndex].priority
                        ? leftChildIndex : rightChildIndex;

                    //더 우선순위가 높은 자식과 부모 노드를 비교 하여
                    //부모가 우선순위가 더 낮은 경우 바꾸기
                    if (nodes[lessChildIndex].priority < nodes[index].priority)
                    {
                        nodes[index] = nodes[lessChildIndex];
                        nodes[lessChildIndex] = lastNode;
                        index = lessChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                //자식 하나 (배열 이용하기 때문에 이 경우 왼쪽 자식만 있게 됨)
                else if (leftChildIndex < nodes.Count) // && rightChildIndex >= nodes.Count 는 앞에서 걸렀으니까
                {
                    if (nodes[leftChildIndex].priority < nodes[index].priority )
                    {
                        nodes[index] = nodes[leftChildIndex];
                        nodes[leftChildIndex] = lastNode;
                        index = leftChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                //자식 없음
                else
                {
                    break;
                }
            }
            return rootNode.element;
        }
        public TElement Peek()
        {
            return nodes[0].element;
        }
        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }
        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }
        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }
    }
}
