using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure 
{ 
    internal class Program
    {
        /*
         * 힙(자료구조)
         * 부모 노드가 항상 자식노드보다 우선순위가 높은 속성을 만족하는 트리기반의 자료구조
         * 많은 자료 중 우선순위가 가장 높은 요소를 빠르게 가져오기 위해 사용
         */
        //토너먼트 형식으로 우선순위 제일 높은 애 한명을 추려냄 <- 이게 힙이고
        //힙을 이용한 어댑터가 우선순위 큐임

        static void PriorityQueue2()
        {
            PriorityQueue<string, int> pq = new PriorityQueue<string, int>();
            //자료형은 string, 우선순위를 결정하기 위한 키 int(비교 연산이 가능한 애면 됨)
            //큐처럼 담아두는데 가장 먼저 나오는 애는 우선순위가 가장 높은 애
            pq.Enqueue("감자", 3);
            pq.Enqueue("옥수수", 1);
            pq.Enqueue("고구마", 2);
            //ascending 오름차순임
            //descending 내림차순은 (Comparer<int>.Create((a, b) => b - a));
            //가산점으로 ai를 만든다? 위쪽 100 오른쪽 10 왼쪽 70 아래쪽 20 해서 위쪽으로 가게끔
            //우선순위가 같으면 나중에 들어간 애가 먼저 나옴

            //뿌리로 부터 파생시켜서 각각의 branch를 갖고 그 branch가 branch를 갖는 거 -> 트리기반
            //하나의 노드가 여러 자식들을 가질 수 있다
            //1. 부모가 여러 자식들을 가질 수도 있다
            //2. 자식이 부모를 갖는 역순은 안 된다(순환X 만약 순환 갖고 있으면 그래프임)
            //선형자료구조가 아닌 트리나 그래프들은 비선형자료구조임
            //힙은 이진트리임 부모 > 자식들 힙은 트리기반이다

            //시간 복잡도
            // 탐색(우선순위 높은 애)      추가       삭제
            // O(1)                      O(logN)    O(logN)
            //앗 왜 그런지 집중 안 해서 못 들음 ㅎㅎ;;
            //1은 우선순위 따라서 인덱스로 바로 찾아가니까 1이고
            //토너먼트라 log된 거 아닌가 이분법적인 처리 들어가니까 logNㅇㅇ
            while (pq.Count > 0)
            {
                Console.WriteLine(pq.Dequeue()); //우선순위가 높은 순서대로 데이터 출력
                //output : 옥수수\n고구마\n감자
            }
        }
        static void Main(string[] args)
        {
            
        }
    }
}