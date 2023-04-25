namespace _07._BinarySearchTree
{
    internal class Program
    {
        /*
         * 트리
         * 계층적인 자료를 나타내는데 자주 사용되는 자료구조
         * 부모노드가 0개 이상의 자식노드를 가질 수 있음
         * 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가질 수 없음
         */

        /*
         * 이진탐색트리
         * 이진속성과 탐색속성을 적용한 트리
         * 이진탐색을 통한 탐색영역을 절반으로 줄여가며 탐색 가능
         * 이진 : 부모노드는 최대 2개의 자식노드들을 가질 수 있음
         * 탐색 : 자신의 노드보다 작은 값들은 왼쪽, 큰 값들은 오른쪽에 위치
         * (더 작은 값 찾으려면 왼쪽으로 가면 되고 큰 값은 오른쪽으로 가면 됨)
         */

        /*
         * 이진탐색트리 시간 복잡도
         * 접근        탐색         삽입        삭제
         * 0(log n)    0(log n)    0(log n)    0(log n)
         * 반분할적인 탐색이 가능하기 때문에 log n
         * 삽입 삭제 빠르길 원했다 - 링크드리스트 / 접근 - 리스트
         */
        void BinarySearchTree()
        {
            //키 == 데이터
            SortedSet<int> sortedSet = new SortedSet<int>();
            //이진탐색트리는 Set이란 이름을 많이 쓰고 정렬이 보장된 자료라 SortedSet
            //맨 왼쪽에서 오른쪽으로 읽으면 정렬이 된다는 듯ㅇㅇ

            //추가
            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);
            sortedSet.Add(4);
            sortedSet.Add(5);

            //탐색
            int searchValue1;
            bool find = sortedSet.TryGetValue(3, out searchValue1); //없을 수도 잇으니 tryGetValue

            //삭제
            sortedSet.Remove(3);

            //탐색용 키(int), 실제 데이터(string)
            SortedDictionary<int, string> sortedDic = new SortedDictionary<int, string>();

            //얘도 노드 기반이라 소외됨 대신 대규모 자료의 탐색 시 많이쓰임 Dictionary가 많이 쓰임
            //Dictionary형태의 자료는 인덱서

            // <이진탐색트리의 주의점>
            // 이진탐색트리의 노드들이 한쪽 자식으로만 추가되는 불균형 발생 가능
            // 이 경우 탐색영역이 절반으로 줄여지지 않기 때문에 시간복잡도 증가
            // 이러한 현상을 막기 위해 자가균형기능을 추가한 트리의 사용이 일반적
            // 대표적인 방식으로 Red-Black Tree, AVL Tree 등이 있음
            
            
        }
        static void Main(string[] args)
        {
            
        }
    }
}