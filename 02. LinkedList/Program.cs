namespace _02._LinkedList
{
    internal class Program
    {
        /*
         * 연결리스트
         * 
         * 데이터를 포함하는 노드들을 연결식으로 만든 자료구조
         * 노드는 데이터와 이전/다음 노드 객체를 참조하고 있음
         * 노드가 메모리에 연속적으로 배치되지 않고 이전/다음 노드의 위치를 확인
         */
        //힙영역에 차례대로 놓지 말고 따로따로 랜덤하게
        //1번이 2번을 참조 2번이 3번을 참조
        //삽입 삭제시에 옮겨주는(당겨오는) 작업을 안 해도 됨

        // <링크드리스트 사용>
        void LinkedList()
        {
            LinkedList<string> linkedList = new LinkedList<string>();

            // 링크드리스트 요소 삽입
            linkedList.AddFirst("0번 앞데이터");
            linkedList.AddFirst("1번 앞데이터");
            linkedList.AddLast("0번 뒤데이터");
            linkedList.AddLast("1번 뒤데이터");

            // 링크드리스트 요소 삭제 -> 얘는 O(n)이 맞음
            linkedList.Remove("1번 앞데이터");

            // 링크드리스트 요소 탐색
            LinkedListNode<string> findNode = linkedList.Find("0번 뒤데이터");

            /*
             * class Node { 이전 주소 / 데이터 / 다음 주소 }
             * 리스트는 배열 기반 링크드 리스트는 노드 기반
             */
            // 링크드리스트 노드를 통한 노드 참조
            LinkedListNode<string> prevNode = findNode.Previous;
            LinkedListNode<string> nextNode = findNode.Next;

            // 링크드리스트 노드를 통한 노드 삽입
            //찾은 노드의 앞에 / 뒤에 -> 앞 / 중간 / 뒤
            linkedList.AddBefore(findNode, "찾은노드 앞데이터");
            linkedList.AddAfter(findNode, "찾은노드 뒤데이터");

            // 링크드리스트 노드를 통한 삭제 -> 얘는 O(1)
            linkedList.Remove(findNode);

            //리스트 -> 동적인 크기의 배열
            //링크드리스트 -> 삽입 삭제 빨라야해
        }

        // <LinkedList의 시간복잡도>
        // 접근		탐색		삽입		삭제
        // O(n)		O(n)	O(1)	O(1)

        //접근에 왜 n만큼 걸리냐 배열은 3번째 찾는다면 메모리 크기만큼을 가면 됐었음
        //연결리스트는 3번째 찾는다면 0번, 1번, 2번 통해서 가는 수밖에 없음
        //삽입, 삭제시에 접근은 이미 된 상태에서 함 노드가 특정되어있는 상태
        //찾아서 삭제해줘 -> 연결리스트 안쓴다
        //가비지 컬렉터에 부담 준다 c#은 가비지컬렉터가 있으니까 찬밥신세 C++에선 호황
        //리스트 - 몬스터, 링크드 리스트 - 몬스터들이 잠깐 나왔다가 들어가는 경우, 탄막게임(삽입 삭제 빠르다)
        //배열 - 장비 부위
        //그냥 리스트 써라 ~~

        static void Main(string[] args)
        {
            
        }
        //연결리스트의 종류
        //단반향 -> 순차적인 작업만한다 노드가 그 방향 주소를 안 갖고 있어 데이터 절감
        //양방향 -> 일반적 -> 오늘 구현할 거
        //C#의 연결리스트인 환형리스트 -> 마지막에가 다음 주소로 첫 번째 애, 첫 번째는 이전주소로 마지막애를 가리킴
    }
}